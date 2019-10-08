using System;
using System.Collections.Generic;
using System.Text;
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
        public void ValidateStudentRecord_WithoutSubject_ReturnFalse()
        {
            Validator validator = new Validator();

            List<Subject> grades = new List<Subject>();

            Subject subject = new Subject();

            subject.SubjectName = "Limba";

            grades.Add(subject);

            StudentModel studentModel = new StudentModel {FirstName = "Chrila",LastName = "Petru",Grades = grades};

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
       
    }
}
