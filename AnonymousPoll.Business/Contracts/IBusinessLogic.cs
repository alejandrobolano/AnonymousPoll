using System.Collections.Generic;
using System.Text;
using AnonymousPoll.Common.Models;

namespace AnonymousPoll.Business.Contracts
{
    public interface IBusinessLogic
    {
        List<string> GetNameOfStudentsIdentified(Student partialStudent);
        Dictionary<int, Student> GetPartialStudentsConverted(List<string> casesTyped);
        StringBuilder GetNamesAppended(List<string> names);
    }
}
