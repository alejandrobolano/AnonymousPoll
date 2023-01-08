using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AnonymousPoll.Business.Contracts;
using AnonymousPoll.Business.Implementations;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Implementations;
using AnonymousPoll.DataAccess.Contracts;
using AnonymousPoll.DataAccess.Implementations;

namespace AnonymousPoll.Test
{
    [TestClass]
    public class BusinessLogicTest
    {
        private static IBusinessLogic _businessLogic;
        private static IDataAccess _dataAccess;
        private static IStudentLogic _studentLogic;
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            _studentLogic = new StudentLogic();
            _dataAccess = new TxtAccess(_studentLogic);
            _businessLogic = new BusinessLogic(_dataAccess,_studentLogic);
        }

        [TestMethod()]
        public void GetNamesAppendedTest_WhenListIsNullOrEmpty()
        {
            const string appendedExpected = "NONE";

            #region null

            var result = _businessLogic.GetNamesAppended(null);
            Assert.AreEqual(appendedExpected, result.ToString());

            #endregion

            #region empty

            var names = new List<string>();
            result = _businessLogic.GetNamesAppended(names);
            Assert.AreEqual(appendedExpected, result.ToString());

            #endregion

        }
        [TestMethod()]
        public void GetNamesAppendedTest()
        {
            var names = GetNamesHelper();
            var result = _businessLogic.GetNamesAppended(names);
            var appendedExpected = GetNamesExpectedHelper();
            Assert.AreEqual(appendedExpected.ToString(), result.ToString());
        }

        #region helper
        private static StringBuilder GetNamesExpectedHelper()
        {
            return new StringBuilder("alfa,beta,gamma");
        }

        private static List<string> GetNamesHelper()
        {
            return new List<string> { "beta", "gamma", "alfa" };
        }
        
        #endregion
    }
}
