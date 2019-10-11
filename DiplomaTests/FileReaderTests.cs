using System.Collections.Generic;
using Xunit;
using Diploma.Files;
using Diploma.Wrappers;
using Moq;
using System.IO;
using Diploma.Models;
using System;

namespace DiplomaTests
{
    public class FileReaderTests
    {
        [Fact]
        public void ImportData_ShouldReturn_CorrectListStudentsRawModel()
        {
            var students = new List<StudentRawModel>()
            {
                new StudentRawModel{Nume = "Albu", Prenume = "Maria", LimbaRomana = 9.8, LimbaEngleza = 9.2, Matematica = 9.1, Fizica = 9.5, Chimia = 10,Informatica = 10, Geografia = 10, Istoria = 10, Biologia = 9.8},
                new StudentRawModel{Nume = "Badulescu" , Prenume = "Mihai", LimbaRomana = 10, LimbaEngleza = 8, Matematica = 8.6, Fizica = 9.8, Chimia = 9, Informatica = 9.7, Geografia = 9.8, Istoria = 8.7, Biologia =  8.9}
            };

            var iFileWrapper = new Mock<IFileWrapper>();
            string path= Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "StudentCatalog.csv");

            iFileWrapper.Setup(a => a.OpenText(path)).Returns(File.OpenText(path));

            var fileReader = new FileReader(iFileWrapper.Object);

            var act = fileReader.ImportData(path);

            Assert.Equal(students.ToString(), act.ToString());
        }

        [Fact]
        public void ImportData_CallingOpenTextWithFileWrapper_Once()
        {
            
            var fileWrapper = new Mock<IFileWrapper>();
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "StudentCatalog.csv");
            fileWrapper.Setup(a => a.OpenText(path)).Returns(File.OpenText(path));
            var fileReaderr = new FileReader(fileWrapper.Object);
            fileReaderr.ImportData(path);
            fileWrapper.Verify(f => f.OpenText(path), Times.Once);
        }
    }
}
