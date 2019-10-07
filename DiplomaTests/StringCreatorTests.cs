
using System.Collections.Generic;
using Xunit;
using Diploma.Models;
using Moq;
using Diploma.Utils;
using Diploma.Wrappers;
using Diploma;

namespace DiplomaTests
{
    public class StringCreatorTests
    {
        [Fact]
        public void GetSecondParagraphText_ShoulReturn_CorrectString()
        {
            var constants = new Mock<IConstants>();

            var domainWrapper = new Mock<IDomainWrapper>();

            var stringcreator = new StringCreator(constants.Object, domainWrapper.Object);

            Subject limba = new Subject { SubjectName = "Limba romana", Grade = 10 };

            List<Subject> grades = new List<Subject>();

            grades.Add(limba);

            var act = stringcreator.GetSecondParagraphText(grades);

            Assert.Equal("Limba romana 10\n", act);
        }
    }
}
