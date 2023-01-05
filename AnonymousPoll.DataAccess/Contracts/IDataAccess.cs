using System.Collections.Generic;
using AnonymousPoll.Common.Models;

namespace AnonymousPoll.DataAccess.Contracts
{
    public interface IDataAccess
    {
        List<Student> GetStudents(Student partialStudent);
    }
}
