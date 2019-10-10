using Diploma.Files;
using Diploma.Utils;
using Diploma.Wrappers;
using Moq;

namespace DiplomaTests.Builder
{
    public class FileWriterBuilder
    {
        private Mock<IDocWrapper> dockWrapper;
        private Mock<IStringCreator> stringCreator;

        public FileWriterBuilder()
        {
            dockWrapper = new Mock<IDocWrapper>();
            stringCreator = new Mock<IStringCreator>();
        }
        
        public FileWriterBuilder WithDockWrapper (Mock<IDocWrapper> dockWrapper)
        {
            this.dockWrapper = dockWrapper;
            return this;
        }
        public FileWriterBuilder WithStringCreator(Mock<IStringCreator> stringCreator)
        {
            this.stringCreator = stringCreator;
            return this;
        }
        public FileWriter Build()
        {
            return new FileWriter(dockWrapper.Object, stringCreator.Object);
        }
    }
}
