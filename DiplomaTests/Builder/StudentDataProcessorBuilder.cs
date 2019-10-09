using Moq;
using Diploma.Files;
using Diploma.Utils;
using Diploma.Validators;
using Diploma.DataProcessing;

namespace DiplomaTests.Builder
{
    public class StudentDataProcessorBuilder
    {
        private Mock<IFileReader> fileReader;
        private Mock<ICustomMapper> mapper;
        private Mock<IFileWriter> fileWriter;
        private Mock<IValidator> validator;

        public StudentDataProcessorBuilder()
        {
            this.fileReader = new Mock<IFileReader>();
            this.mapper = new Mock<ICustomMapper>();
            this.fileWriter = new Mock<IFileWriter>();
            this.validator = new Mock<IValidator>();
        }

        public StudentDataProcessorBuilder WithFileReader (IFileReader fileReader)
        {
            this.fileReader = new Mock<IFileReader>(fileReader);
            return this;
        }
        public StudentDataProcessorBuilder WithMapper(ICustomMapper mapper)
        {
            this.mapper = new Mock<ICustomMapper>(mapper);
            return this;
        }
        public StudentDataProcessorBuilder WithFileWriter(IFileWriter fileWriter)
        {
            this.fileWriter = new Mock<IFileWriter>(fileWriter);
            return this;
        }
        public StudentDataProcessorBuilder WithValidator(IValidator validator)
        {
            this.validator = new Mock<IValidator>(validator);
            return this;
        }
        public StudentDataProcessor Build()
        {
            return new StudentDataProcessor(fileReader.Object, mapper.Object, fileWriter.Object, validator.Object);
        }
    }
}
