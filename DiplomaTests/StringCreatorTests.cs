
using System.Collections.Generic;
using Xunit;
using Diploma.Models;
using DiplomaTests.Builder;
using Moq;
using Diploma.Utils;
using Diploma.Wrappers;
using Diploma;
using FluentAssertions;
using System;

namespace DiplomaTests
{
    public class StringCreatorTests
    {
      
        [Theory]
        [InlineData("Limba romana",10,"Limba romana 10\n")]
        [InlineData("Limba engleza",7.8,"Limba engleza 7.8\n")]
        [InlineData("Mate",9.3,"Mate 9.3\n")]
        [InlineData("Fizica",7,"Fizica 7\n")]
        [InlineData("Chimia",10,"Chimia 10\n")]
        public void GetSecondParagraphText_ShoultReturn_CorrectString(string subject,double grade,string expected)
        {
            var stringCreator = new StringCreatorBuilder().Build();
            Subject subjectexample = new Subject { SubjectName = subject, Grade = grade };

            List<Subject> subjects = new List<Subject>
            {
                subjectexample
            };

            var act = stringCreator.GetSecondParagraphText(subjects);

            act.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("Movila","Sergiu", "Diploma \n\n Studii Medii\nMovila Sergiu")]
        [InlineData("Chrila","Petru", "Diploma \n\n Studii Medii\nChrila Petru")]
        [InlineData("Cojocari","Liviu", "Diploma \n\n Studii Medii\nCojocari Liviu")]
        [InlineData("Ropot","Andrei", "Diploma \n\n Studii Medii\nRopot Andrei")]
        [InlineData("Popov","Catalin", "Diploma \n\n Studii Medii\nPopov Catalin")]   
        public void GetFirstParagraph_ShoulReturn_CorrectString(string name ,string surname ,string expected)
        {
            var constant = new Mock<IConstants>();
            constant.Setup(s => s.GetDiplomNameText).Returns("Diploma \n\n Studii Medii");
            var stringCreator = new StringCreatorBuilder().WithConstants(constant).Build();
            var act = stringCreator.GetFirstParagraph(name, surname);

            Assert.Equal(expected, act);
        }

        [Theory]
        [InlineData("Movila", "Sergiu")]
        [InlineData("Chrila", "Petru")]
        [InlineData("Cojocari", "Liviu")]
        [InlineData("Ropot", "Andrei")]
        [InlineData("Popov", "Catalin")]
        public void CreatePath_ShouldReturn_CorrectString(string name, string surname)
        {
            var domain = new Mock<IDomainWrapper>();
            var constants = new Mock<IConstants>();
            constants.Setup(c => c.GetExtension).Returns(".docx");
            domain.Setup(d => d.GetRoothDirectory()).Returns(AppDomain.CurrentDomain.BaseDirectory);
            var sut = new StringCreatorBuilder().WithDomain(domain).WithConstants(constants).Build();

            var act = sut.CreatePath(name, surname);
            var expected = AppDomain.CurrentDomain.BaseDirectory + name + surname + ".docx";
            Assert.Equal(expected, act);
        }
    }
}
