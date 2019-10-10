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

        public StudentDataProcessorBuilder WithFileReader (Mock<IFileReader> fileReader)
        {
            this.fileReader = fileReader;
            return this;
        }
        public StudentDataProcessorBuilder WithMapper(Mock<ICustomMapper> mapper)
        {
            this.mapper = mapper;
            return this;
        }
        public StudentDataProcessorBuilder WithFileWriter(Mock<IFileWriter> fileWriter)
        {
            this.fileWriter = fileWriter;
            return this;
        }
        public StudentDataProcessorBuilder WithValidator(Mock<IValidator> validator)
        {
            this.validator = validator;
            return this;
        }
        public StudentDataProcessor Build()
        {
            return new StudentDataProcessor(fileReader.Object, mapper.Object, fileWriter.Object, validator.Object);
        }
    }
}
