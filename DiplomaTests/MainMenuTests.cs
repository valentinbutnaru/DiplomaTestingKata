using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Diploma;
using Diploma.DataProcessing;
using DiplomaTests.Builder;
using Moq;

namespace DiplomaTests
{
    public class MainMenuTests
    {
        [Fact]
        public void CollectData_CallingConsoleWrappedWriteLine_WithGetHelloText_Once()
        {
            var console = new Mock<IConsoleWrapper>();
            var constants = new Mock<IConstants>();

            constants.Setup(c => c.GetHelloText).Returns("Hello");

            var sut = new MainMenuBuilder().WithConstants(constants).WithConsoleWrapper(console).Build();

            sut.CollectData();
            console.Verify(c => c.WriteLine("Hello"), Times.Once);
        }

        [Fact]
        public void CollectData_CallingReadkey_WithEnteredKey_Once()
        {
            var console = new Mock<IConsoleWrapper>();

            var sut = new MainMenuBuilder().WithConsoleWrapper(console).Build();

            sut.CollectData();
            console.Verify(c => c.ReadKey(), Times.Once);
        }

        [Fact]
        public void CollectData_CallingConsoleWrappedWriteLine_GetImportFileText_Once()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            var console = new Mock<IConsoleWrapper>();
            var constants = new Mock<IConstants>();
            constants.Setup(c => c.GetImportFileText).Returns("HI");
            console.Setup(c => c.ReadKey()).Returns(keyInfo);
            var sut = new MainMenuBuilder().WithConsoleWrapper(console).WithConstants(constants).Build();
            sut.CollectData();
            console.Verify(c => c.WriteLine("HI"), Times.Once);
        }

        [Fact]
        public void CollectData_CallingReadLine_Once()
        {
            var console = new Mock<IConsoleWrapper>();

            var sut = new MainMenuBuilder().WithConsoleWrapper(console).Build();

            sut.CollectData();

            console.Verify(c => c.ReadLine(), Times.Once);
        }

        [Fact]
        public void CollectData_CallingLoadData_Once()
        {
            var loader = new Mock<IStudentDataProcessor>();

            var sut = new MainMenuBuilder().WithStudentDataProcessor(loader).Build();

            loader.Verify(l => l.LoadData(It.IsAny<string>()), Times.Once);
           
        }

        [Fact]
        public void CollectData_CallingEndOfProgram_Once()
        {
            var constant = new Mock<IConstants>();
            constant.Setup(c => c.GetExitText).Returns("Exit");
            var console = new Mock<IConsoleWrapper>();

            var sut = new MainMenuBuilder().WithConsoleWrapper(console).WithConstants(constant).Build();

            sut.CollectData();

            console.Verify(c => c.WriteLine("Exit"), Times.Once);
        }
    }
}
