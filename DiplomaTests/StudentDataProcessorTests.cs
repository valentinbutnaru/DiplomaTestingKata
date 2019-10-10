using Xunit;
using Moq;
using System.Collections.Generic;
using Diploma.Files;
using DiplomaTests.Builder;
using Diploma.Utils;
using Diploma.Models;
using Diploma.Validators;
using GemBox.Document;
using FluentAssertions;
using Diploma.DataProcessing;

namespace DiplomaTests
{
    public class StudentDataProcessorTests
    {
        [Fact]
        public void LoadData_CallLoadaData_OneTime()
        {
            string path = "path";
            var fileReader = new Mock<IFileReader>();
            
            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader).Build();

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
            var sut = new StudentDataProcessorBuilder().WithFileReader(fileReader).WithMapper(mapper).Build();

            sut.LoadData(path);
            mapper.Verify(m => m.MapToStudent(students), Times.Once());
        }

        [Fact]
        public void LoadData_CallValidator_WithStudentsFromMapper_AtLeastOnce()
        {
            string path = "path";
            var mappedStudents = new List<StudentModel> { new StudentModel() };
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithValidator(validator).Build();

            sut.LoadData(path);
            validator.Verify(v => v.ValidateStudentRecord(It.IsAny<StudentModel>()), Times.AtLeastOnce());
        }

        [Fact]
        public void LoadData_CallCreateDiplomas_WithStudentFromMapper_AtLeastOnce()
        {
            string path = "path";
            var mappedStudents = new List<StudentModel> { new StudentModel() };
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            var fileWritter = new Mock<IFileWriter>();
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithFileWriter(fileWritter).WithValidator(validator).Build();

            sut.LoadData(path);
            fileWritter.Verify(f => f.CreateDiplomas(It.IsAny<StudentModel>()), Times.AtLeastOnce());
        }

        [Fact]
        public void LoadData_CallCreateDiplomas_WithStudentFromMapper_ExactlyTimes()
        {
            string path = "path";
            var mappedStudents = new List<StudentModel> { new StudentModel(), new StudentModel(), new StudentModel() };
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            var fileWritter = new Mock<IFileWriter>();
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithFileWriter(fileWritter).WithValidator(validator).Build();

            sut.LoadData(path);
            fileWritter.Verify(f => f.CreateDiplomas(It.IsAny<StudentModel>()), Times.Exactly(3));
        }

        [Fact]
        public void LoadData_SaveDiploma_WithStudentDiploma_AtLeastOnce()
        {
            string path = "path";
            DocumentModel studentDiplomas = null;
            var mappedStudents = new List<StudentModel> { new StudentModel() };
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var fileWritter = new Mock<IFileWriter>();
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithValidator(validator).WithFileWriter(fileWritter).Build();

            sut.LoadData(path);

            fileWritter.Verify(f => f.SaveDiploma(studentDiplomas, It.IsAny<string>(),It.IsAny<string>()),Times.AtLeastOnce);
        }

        [Fact]
        public void LoadData_CallsValidator_WithAllStudentsExactly()
        {
            string path = "path";
            var mappedStudents = new List<StudentModel> { new StudentModel(), new StudentModel(), new StudentModel() };
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithValidator(validator).Build();

            sut.LoadData(path);
            validator.Verify(v => v.ValidateStudentRecord(It.IsAny<StudentModel>()), Times.Exactly(3));
        }

        [Fact]
        public void LoadData_SaveDiploma_WithStudentDiploma_ExactlyTimes()
        {
            string path = "path";
            DocumentModel studentDiplomas = null;
            var mappedStudents = new List<StudentModel> { new StudentModel(), new StudentModel(), new StudentModel()};
            var mapper = new Mock<ICustomMapper>();
            mapper.Setup(m => m.MapToStudent(It.IsAny<IEnumerable<StudentRawModel>>())).Returns(mappedStudents);
            var validator = new Mock<IValidator>();
            var fileWritter = new Mock<IFileWriter>();
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            validator.Setup(v => v.ValidateStudentRecord(It.IsAny<StudentModel>())).Returns(true);
            var sut = new StudentDataProcessorBuilder().WithMapper(mapper).WithValidator(validator).WithFileWriter(fileWritter).Build();

            sut.LoadData(path);

            fileWritter.Verify(f => f.SaveDiploma(studentDiplomas, It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }
    }
}