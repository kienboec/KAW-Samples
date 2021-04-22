using System;
using System.IO;
using NUnit.Framework;
using QuestionService.Common;
using QuestionService.AnswerLogger;
using QuestionService.AnswerLogger.Test.Mock;

namespace QuestionService.AnswerLogger.Test
{
    public class AnswerLoggerTests
    {
        [Test]
        public void Given_AMessageIsReceived_When_Answer1_Then_OutputWith1() // Äquivalenzklasse "korrekte Werte" : 1-4
        {
            // arrange
            AnswerSentMessage message = new AnswerSentMessage() { AnswerIndex = 1 };
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: 1", actual);
        }
        
        [Test]
        public void Given_AMessageIsReceived_When_AnswerIsNull_Then_OutputWithError() // Äquivalenzklasse "null" 
        {
            // arrange
            AnswerSentMessage message = null;
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: Error", actual);
        }

        [Test]
        public void Given_AMessageIsReceived_When_AnswerIs5_Then_OutputWithError() // Äquivalenzklasse "ungültiger Wert" : nicht 1-4
        {
            // arrange
            AnswerSentMessage message = new AnswerSentMessage() { AnswerIndex = 5 };
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: Error", actual);
        }

        [Test]
        public void Given_AMessageIsReceived_When_AnswerIsMinus1_Then_OutputWithError() // Äquivalenzklasse "ungültiger Wert" : nicht 1-4
        {
            // arrange
            AnswerSentMessage message = new AnswerSentMessage() { AnswerIndex = -1 };
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: Error", actual);
        }

        [Test]
        public void Given_AMessageIsReceived_When_AnswerIsDefault_Then_OutputWithError() // Äquivalenzklasse "uninitialisiert"
        {
            // arrange
            AnswerSentMessage message = new AnswerSentMessage() { };
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: Error", actual);
        }

        [Test]
        public void Given_AMessageIsReceived_When_AnswerIsSetToDefaultManually_Then_OutputWithError() // Äquivalenzklasse "uninitialisiert"
        {
            // arrange
            AnswerSentMessage message = new AnswerSentMessage() { AnswerIndex = 0};
            AnswerSentTextFormatter formatter = new AnswerSentTextFormatter(message);

            // act
            var actual = formatter.Format();

            // assert
            Assert.IsNotEmpty(actual);
            Assert.IsNotNull(actual);
            Assert.AreEqual("Answer received: Error", actual);
        }

        [Test]
        public void Given_AMessageIsReceivedAndShouldBePrinted_When_WriteMethodWithNullCalled_Then_ExceptionShouldBeThrown() // Äquivalenzklasse "uninitialisiert"
        {
            // arrange
            TextFormatterEmptyMock formatter = new TextFormatterEmptyMock();
            bool isThrown = false;

            // act
            try
            {
                Program.WriteOutputString(null, formatter);
            }
            catch (NullReferenceException exc)
            {
                isThrown = true;
            }

            // assert
            Assert.IsTrue(isThrown);
        }

        [Test]
        public void Given_AMessageIsReceivedAndShouldBePrinted_When_WriteMethodWithTextWriterCalled_Then_TextShouldBeWrittenWithoutException() // Äquivalenzklasse "uninitialisiert"
        {
            // arrange
            TextFormatterEmptyMock formatter = new TextFormatterEmptyMock();
            bool isThrown = false;
            MemoryStream stream = new MemoryStream();
            TextWriter writer = new StreamWriter(stream);

            // act
            try
            {
                Program.WriteOutputString(writer, formatter);
            }
            catch (NullReferenceException exc)
            {
                isThrown = true;
            }

            // assert
            Assert.IsFalse(isThrown);
        }
    }
}