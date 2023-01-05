using AnonymousPoll.Common.Models;

namespace AnonymousPoll.Common.Contracts
{
    public interface IStudentLogic
    {
        Student ConvertStringToStudent(string stream, bool isPartialStudent = true);
    }
}
