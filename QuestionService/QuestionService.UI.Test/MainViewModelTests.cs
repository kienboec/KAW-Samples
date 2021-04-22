using NUnit.Framework;

namespace QuestionService.UI.Test
{
    public class MainViewModelTests
    {
        // Attention in .NET 5 you need to change the target type to be compatible with WPF (.NET5-windows, see: target type UI)


        [Test]
        public void Given_AMainViewModel_When_InitDataInDesignMode_Then_TheStandardQuestionShouldBeShown()
        {
            // arrange
            MainViewModel vm = new MainViewModel(true);

            // act
            var actual = vm.QuestionText;

            // assert
            Assert.AreEqual("How much is the fish?", actual);
        }
    }
}