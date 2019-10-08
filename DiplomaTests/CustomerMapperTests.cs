using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Diploma.Utils;
using Diploma.Models;

namespace DiplomaTests
{
    public class CustomerMapperTests
    {

        [Fact]
        public void MapToStudent_ShouldReturn_CorrectListOfStudentModel()
        {
            var students = new List<StudentRawModel>()
            {
                new StudentRawModel{Nume = "Albu", Prenume = "Maria", LimbaRomana = 9.8, LimbaEngleza = 9.2, Matematica = 9.1, Fizica = 9.5, Chimia = 10,Informatica = 10, Geografia = 10, Istoria = 10, Biologia = 9.8}
            };

            var expected = new List<StudentModel>();

            var grades = new List<Subject>
            {
                new Subject {SubjectName = "LimbaRomana", Grade = 9.8},
                new Subject {SubjectName = "LimbaEngleza", Grade = 9.2},
                new Subject {SubjectName = "Matematica", Grade = 9.1},
                new Subject {SubjectName = "Fizica", Grade = 9.5},
                new Subject {SubjectName = "Chimia", Grade = 10},
                new Subject {SubjectName = "Informatica", Grade = 10},
                new Subject {SubjectName = "Geografia", Grade = 10},
                new Subject {SubjectName = "Istoria", Grade = 10},
                new Subject {SubjectName = "Biologia", Grade = 9.8}
            };

            expected.Add(new StudentModel { FirstName = "Albu", LastName = "Maria", Grades = grades});

            CustomMapper customMapper = new CustomMapper();

            var act = customMapper.MapToStudent(students);

            Assert.Equal(expected.ToString(), act.ToString());

        }
    }
}
