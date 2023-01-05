using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AnonymousPoll.Business.Contracts;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Models;
using AnonymousPoll.DataAccess.Contracts;
using AnonymousPoll.DataAccess.Implementations;
using log4net;

namespace AnonymousPoll.Business.Implementations
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly IDataAccess _dataAccess;
        private readonly IStudentLogic _studentLogic;
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public BusinessLogic(IDataAccess dataAccess, IStudentLogic studentLogic)
        {
            _dataAccess = dataAccess;
            _studentLogic = studentLogic;
        }
        public List<string> GetNameOfStudentsIdentified(Student partialStudent)
        {
            return _dataAccess.GetStudents(partialStudent).Select(student => student.Name).ToList();
        }

        public Dictionary<int, Student> GetPartialStudentsConverted(List<string> casesTyped)
        {
            var result = new Dictionary<int, Student>();
            for (var index = 0; index < casesTyped.Count; index++)
            {
                try
                {
                    var partialCase = casesTyped[index];
                    var partialStudent = _studentLogic.ConvertStringToStudent(partialCase);
                    result.Add(index + 1, partialStudent);
                }
                catch (FormatException formatException)
                {
                    Log.Error(
                        $"An error has occurred with case #{index + 1}: {formatException.Message}. Please check more information in Logs",
                        formatException);
                    throw new FormatException();
                }
                catch (Exception exception)
                {
                    Log.Error("\nAn uncontrollable error has occurred:" + exception.Message, exception);
                }

            }
            return result;
        }

        public StringBuilder GetNamesAppended(List<string> names)
        {
            if (names.Count == 0)
            {
                return new StringBuilder("NONE");
            }
            var result = new StringBuilder("");
            var namesSorted = GetListSortedLexicographically(names);
            for (var index = 0; index < namesSorted.Count; index++)
            {
                var name = namesSorted[index];
                result.Append(name);
                if (index + 1 == namesSorted.Count) break;
                result.Append(",");
            }
            return result;
        }

        private static List<string> GetListSortedLexicographically(IEnumerable<string> names)
        {
            var sort = from name in names orderby name select name;
            return sort.ToList();
        }
    }
}
