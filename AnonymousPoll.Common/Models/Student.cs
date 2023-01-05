namespace AnonymousPoll.Common.Models
{
    public class Student
    {
        public string Name { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
        public string Education { get; set; }
        public int AcademicYear { get; set; }

        public override bool Equals(object o)
        {
            if (!(o is Student student))
            {
                return false;
            }
            return Gender == student.Gender && Age == student.Age && Education == student.Education && AcademicYear == student.AcademicYear;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Gender.GetHashCode();
                hashCode = (hashCode * 397) ^ Age;
                hashCode = (hashCode * 397) ^ (Education != null ? Education.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ AcademicYear;
                return hashCode;
            }
        }
    }
}
