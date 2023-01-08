using System;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnonymousPoll.Test
{
    [TestClass()]
    public class StudentLogicTests
    {
        private static IStudentLogic _studentLogic;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            _studentLogic = new StudentLogic();
        }

        [TestMethod()]
        [DataRow("M,21,Human Resources Management,3")]
        [DataRow("F,20,Systems Engineering,2")]
        public void ConvertStringToStudentTest_WhenIsPartial(string stream)
        {
            var student = _studentLogic.ConvertStringToStudent(stream, true);
            Assert.IsTrue(string.IsNullOrEmpty(student.Name));
            Assert.IsTrue(student.Gender =='F' || student.Gender == 'M');
            Assert.IsTrue(student.Age != 0);
            Assert.IsTrue(!string.IsNullOrEmpty(student.Education));
            Assert.IsTrue(student.AcademicYear != 0);
        }

        [TestMethod()]
        [DataRow("Sophia Wright Torres,F,23,Radiologic Technology,1")]
        [DataRow("Chloe Gomez Green,F,22,Diagnostic Radiography,2")]
        public void ConvertStringToStudentTest(string stream)
        {
            var student = _studentLogic.ConvertStringToStudent(stream, false);
            Assert.IsTrue(!string.IsNullOrEmpty(student.Name));
            Assert.IsTrue(student.Gender =='F' || student.Gender == 'M');
            Assert.IsTrue(student.Age != 0);
            Assert.IsTrue(!string.IsNullOrEmpty(student.Education));
            Assert.IsTrue(student.AcademicYear != 0);
        }

        [TestMethod()]
        [DataRow("A,23,Radiologic Technology,1")]
        [DataRow("F,23,Radiologic Technology,string")]
        public void ConvertStringToStudentTest_WithError(string stream)
        {
            try
            {
                var student = _studentLogic.ConvertStringToStudent(stream, true);
                Assert.Fail("Test has failed");
            }
            catch (FormatException exception)
            {
                Assert.IsTrue(exception.Message.Contains("Format incorrect of"));
            }
        }
    }
}