using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using AnonymousPoll.Common;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Models;
using AnonymousPoll.DataAccess.Contracts;
using log4net;

namespace AnonymousPoll.DataAccess.Implementations
{
    public class TxtAccess : IDataAccess
    {
        private readonly IStudentLogic _studentLogic;
        public static string StudentFile = ConfigurationManager.AppSettings.Get("StudentTxt");
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public TxtAccess(IStudentLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }

        public List<Student> GetStudents(Student partialStudent)
        {
            var result = new List<Student>();
            try
            {
                var lines = File.ReadLines(StudentFile).ToList();
                lines.ForEach(line =>
                {
                    var student = _studentLogic.ConvertStringToStudent(line, false);
                    if (student.Equals(partialStudent))
                    {
                        result.Add(student);
                    }
                });
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                Log.Error($"File {StudentFile} not found. Please check more information in Logs", fileNotFoundException);
                throw;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message, exception);
                throw;

            }

            return result;
        }
    }
}
