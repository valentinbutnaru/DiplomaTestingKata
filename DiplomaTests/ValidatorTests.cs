
using System.Collections.Generic;
using Xunit;
using Diploma.Validators;
using Diploma.Models;

namespace DiplomaTests
{
    public class ValidatorTests
    {
        [Fact]
        public void ValidateStudentRecord_EmptyStudent_ReturnFalse()
        {
            Validator validator = new Validator();

            var studentModel = new StudentModel();

            var actual = validator.ValidateStudentRecord(studentModel);

            Assert.Equal(false, actual);
        }

        [Fact]
        public void ValidateStudentRecord_WithGradesOutOfRange_ReturnFalse()
        {
            Validator validator = new Validator();

            Subject limba = new Subject { SubjectName = "Limba romana", Grade = -10 };

            List<Subject> grades = new List<Subject>();

            grades.Add(limba);

            var studentModel = new StudentModel { FirstName = "Chrila", LastName = "Petru", Grades = grades };

            var act = validator.ValidateStudentRecord(studentModel);

            Assert.Equal(false, act);
        }

        [Fact]
        public void ValidateStudentRecord_ShouldNotReturnFalse()
        {
            Validator validator = new Validator();

            Subject limba = new Subject { SubjectName = "Limba romana", Grade = 10 };

            List<Subject> grades = new List<Subject>();

            grades.Add(limba);

            var studentModel = new StudentModel { FirstName = "Chrila", LastName = "Petru", Grades = grades};

            var actual = validator.ValidateStudentRecord(studentModel);

            Assert.Equal(true, actual);
        }
       
        [Theory]
        [InlineData(null,"Sergiu","Limba romana",10)]
        [InlineData("Movila",null,"Limba romana",5.8)]
        [InlineData("Movila","Sergiu",null,7)]
        [InlineData("","Sergiu","Limba romana",9)]
        [InlineData("Movila","","Limba romana",3)]
        [InlineData("Movila","Sergiu","",9)]
        public void ValidateStudentRecord_MissingOneField_ShouldReturnFalse(string fName,string lName,string subjectName,double grade)
        {
            Validator validator = new Validator();
            Subject l = new Subject { SubjectName = subjectName, Grade = grade };
            List<Subject> grades = new List<Subject>();
            grades.Add(l);
            var studentModel = new StudentModel { FirstName = fName, LastName = lName, Grades = grades };
            var actual = validator.ValidateStudentRecord(studentModel);
            Assert.Equal(false, actual);
        }
    }
}
