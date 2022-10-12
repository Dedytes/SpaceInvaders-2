using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pu17alr_Student;

namespace test_unitaire
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            Student student1 = new Student("Pete", "Cation", 1000);
            // Ajout des notes pour cet étudiant
            student1.AddSchoolMarks(4.5);
            student1.AddSchoolMarks(5.5);
            student1.AddSchoolMarks(5.0);
            double avg;

            //Act

            avg = student1.SchoolMarksAverage();

            //Assert
            Assert.AreEqual(5.0, avg, "La moyenne doit être de 5.0");
        }
    }
}
