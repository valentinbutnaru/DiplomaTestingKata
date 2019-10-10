using System;
using System.Collections.Generic;
using System.Text;
using Diploma.Files;
using Xunit;
using Moq;
using GemBox.Document;
using Diploma.Models;
using Diploma.Utils;
using Diploma.Wrappers;
using DiplomaTests.Builder;
using FluentAssertions;
using Diploma;

namespace DiplomaTests
{
    public class FileWriterTests
    {
        [Fact]
        public void CreateDiplomas_ShouldReturnCorrectDocument()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            var documentModel = new DocumentModel();
            Subject Limba = new Subject { SubjectName = "Limba Romana", Grade = 10 };
            List<Subject> grade = new List<Subject>
            {
                Limba
            };
            StudentModel student = new StudentModel { FirstName = "Movila", LastName = "Sergiu", Grades = grade };
            var stringgCreator = new Mock<IStringCreator>();
            stringgCreator.Setup(s => s.GetFirstParagraph("Movila", "Sergiu")).Returns("Diploma \n\n Studii Medii\nMovila Sergiu");
            stringgCreator.Setup(s => s.GetSecondParagraphText(It.IsAny<IEnumerable<Subject>>())).Returns("Limba romana 10\n");

            //documentModel.Sections.Add(new Section(documentModel,
            //    new List<Paragraph>() { new Paragraph(documentModel, firstparagraph ), new Paragraph(documentModel, secondParagraphText) }));


            var sut = new FileWriterBuilder().WithStringCreator(stringgCreator).Build();
            var act = sut.CreateDiplomas(student);

            act.Should().BeSameAs(documentModel);
        }

        [Fact]
        public void SaveDiploma_CreathPath_CallOnce()
        {
            var stringCreator = new Mock<IStringCreator>();
            var sut = new FileWriterBuilder().WithStringCreator(stringCreator).Build();

            sut.SaveDiploma(It.IsAny<DocumentModel>(), It.IsAny<string>(), It.IsAny<string>());
            stringCreator.Verify(s => s.CreatePath(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SaveDiploma_SaveDocument_CallOnce()
        {
            var docWrapper = new Mock<IDocWrapper>();
            var sut = new FileWriterBuilder().WithDockWrapper(docWrapper).Build();

            sut.SaveDiploma(It.IsAny<DocumentModel>(), It.IsAny<string>(), It.IsAny<string>());
            docWrapper.Verify(d => d.SaveDocument(It.IsAny<DocumentModel>(), It.IsAny<string>()), Times.Once());
        }
    }
}


