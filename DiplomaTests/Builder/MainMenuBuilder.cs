using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Diploma.Utils;
using Diploma.DataProcessing;
using Diploma.Wrappers;
using Diploma;

namespace DiplomaTests.Builder
{
    public class MainMenuBuilder
    {
        private Mock<IConsoleWrapper> consoleWrapper;
        private Mock<IConstants> constants;
        private Mock<IStudentDataProcessor> studentDataProcessor;

        public MainMenuBuilder()
        {
            this.consoleWrapper = new Mock<IConsoleWrapper>();
            this.constants = new Mock<IConstants>();
            this.studentDataProcessor = new Mock<IStudentDataProcessor>();
        }
        public MainMenuBuilder WithConsoleWrapper(Mock<IConsoleWrapper> console)
        {
            this.consoleWrapper = console;
            return this;
        }

        public MainMenuBuilder WithConstants(Mock<IConstants> constants)
        {
            this.constants = constants;
            return this;
        }

        public MainMenuBuilder WithStudentDataProcessor(Mock<IStudentDataProcessor> student)
        {
            this.studentDataProcessor = student;
            return this;
        }
        public MainMenu Build()
        {
            return new MainMenu(consoleWrapper.Object, constants.Object, studentDataProcessor.Object);
        }
    }
}
