using Xunit;
using Moq;
using System.Collections.Generic;
using Diploma.Files;
using DiplomaTests.Builder;
using Diploma.Utils;
using Diploma.Models;
using Diploma.Validators;

namespace DiplomaTests
{
    public class StudentDataProcessorTests
    {
        [Fact]
        public void LoadData_CallLoadaData_OneTime()
        {
            string path = "path";
            var fileReader = new Mock<IFileReader>();

            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader.Object).Build();

            sut.LoadData(path);
            fileReader.Verify(f => f.ImportData(path), Times.Once());
        }

        [Fact]
        public void LoadData_CallMapper_WithStudents()
        {
            string path = "path";
            var fileReader = new Mock<IFileReader>();
            var students = new List<StudentRawModel> { new StudentRawModel() };
            fileReader.Setup(a => a.ImportData(path)).Returns(students);
            var mapper = new Mock<ICustomMapper>();
            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader.Object).WithMapper(mapper.Object).Build();

            sut.LoadData(path);
            mapper.Verify(m => m.MapToStudent(students), Times.Once());
        }

        [Fact]
        public void LoadData_CallValidator_WithStudentsFromMapper()
        {
            string path = "path";
            var fileReader = new Mock<IFileReader>();
            var students = new List<StudentRawModel> { new StudentRawModel() };
            var mappedStudents = new List<StudentModel> { new StudentModel() };
            fileReader.Setup(f => f.ImportData(path)).Returns(students);
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(students)).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader.Object).WithMapper(mapper.Object).WithValidator(validator.Object).Build();

            sut.LoadData(path);
            foreach(var student in mappedStudents)
            validator.Verify(v => v.ValidateStudentRecord(student), Times.AtLeastOnce());
        }

        [Fact]
        public void LoadData_CallCreateDiplomas_WithStudentFromMapper()
        {
            string path = "path";
            var fileReader = new Mock<IFileReader>();
            var students = new List<StudentRawModel> { new StudentRawModel() };
            var mappedStudents = new List<StudentModel> { new StudentModel() };
            fileReader.Setup(f => f.ImportData(path)).Returns(students);
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(students)).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var fileWritter = new Mock<IFileWriter>();
            foreach (var student in mappedStudents)
                validator.Setup(v => v.ValidateStudentRecord(student)).Returns(true);
            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader.Object).WithMapper(mapper.Object).WithValidator(validator.Object).WithFileWriter(fileWritter.Object).Build();

            sut.LoadData(path);
            foreach (var student in mappedStudents)
                fileWritter.Verify(f => f.CreateDiplomas(student), Times.AtLeastOnce());
        }
    }
}
