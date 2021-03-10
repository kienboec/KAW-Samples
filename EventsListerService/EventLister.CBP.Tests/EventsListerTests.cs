using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsLister.CBP;
using Moq;
using NUnit.Framework;

namespace EventLister.CBP.Tests
{
    public class Tests
    {
        [Test]
        public async Task ShowList()
        {
            var argumentHandler = (IArgumentHandler)new ArgumentHandler(new string[] { });

            var ioHandler = new Mock<IIOHandler>();
            var ioHandlerCommandsQueue = new Queue<string>(new string[] { "list", "exit" });
            var ioHandlerOutputs = new List<string>();
            ioHandler
                .Setup(x => x.ReadLine())
                .Returns(() => ioHandlerCommandsQueue.Dequeue());

            ioHandler
                .Setup(x => x.WriteLine(It.IsAny<string>()))
                .Callback(new Action<string>(text => ioHandlerOutputs.Add(text)));

            ioHandler
                .Setup(x => x.Write(It.IsAny<string>()))
                .Callback(new Action<string>(text => ioHandlerOutputs.Add(text)));

            var httpOutputInterpreter = new Mock<HTTPOutputInterpreter> {CallBase = true};

            var networkCommunicationHandler = new Mock<INetworkCommunicationHandler>();
            networkCommunicationHandler.Setup(x => x.GetHttpContentAsync(argumentHandler.EventPageUrl)).Returns(Task.FromResult(
                #region HTML Page
@"<html><head></head><body>

<div id=""node-12997"" class=""node node-news de termin clearfix"">
    <div class=""date-huge"">
        <div class=""month"">Feb</div>
        <div class=""day"">
              12          </div>
    </div>
    <div class=""title"">
              <h3>
        <a href=""/newsroom/veranstaltungen/lehrerinnenfortbildung-2020-education-4-0/"">
          LehrerInnenfortbildung 2020: Education 4.0        </a>
      </h3>
        
        <div class=""submitted"">
            in                  1 Tagen
        </div>
    </div>
  <div class=""clearfix""></div>

  <div class=""date-block"">
    <div class=""day"">
      Mittwoch,
      12.
      Februar      2020    </div>
          <div class=""time"">
        09:00 - 16:00      </div>
              <div class=""location"">
        
  Ort: FH Technikum Wien, Höchstädtplatz 6, 1200 Wien       </div>
      </div>

  <div class=""content"">
    
  <div class=""field field-name-field-teaser-text field-type-text-long field-label-hidden even third-0"">
    Die FH Technikum Wien als Österreichs größte, rein technische Fachhochschule bietet mit ihren Studiengängen Mechatronik/Robotik, Maschinenbau und Internationales Wirtschaftsingenieurwesen ( Master, Tagesform und Abendform) einen kostenlosen Fortbildungstag für LehrerInnen an.
  </div>
        <a class=""more-link"" href=""/newsroom/veranstaltungen/lehrerinnenfortbildung-2020-education-4-0/""><i class=""fa fa-chevron-right""></i> mehr</a>
  </div>
</div>


</body>
</html>"
                #endregion
            ));

            var uiHandler = (IUIHandler)new UIHandler(argumentHandler, ioHandler.Object, networkCommunicationHandler.Object, httpOutputInterpreter.Object);
            await uiHandler.InitAsync();

            httpOutputInterpreter.Verify(x => x.InterpretEventPage(It.IsAny<string>()), Times.Exactly(1));
            CollectionAssert.Contains(ioHandlerOutputs, "found 1 dates:");
            CollectionAssert.Contains(ioHandlerOutputs, "- 12.Feb: LehrerInnenfortbildung 2020: Education 4.0");
        }
    }
}