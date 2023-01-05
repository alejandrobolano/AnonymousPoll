using System;
using AnonymousPoll.Common.Contracts;
using AnonymousPoll.Common.Models;

namespace AnonymousPoll.Common.Implementations
{
    public class StudentLogic : IStudentLogic
    {
        private const string FormatIncorrectMessage = "Format incorrect of";

        public Student ConvertStringToStudent(string stream, bool isPartialStudent = true)
        {
            var student = new Student();
            var streamList = stream.Split(',');
            var position = -1;
            if (!isPartialStudent)
            {
                student.Name = streamList[++position];
            }
            var isCharacter = char.TryParse(streamList[++position], out char gender);
            if (!isCharacter || !IsGenderFormat(gender)) throw new FormatException($"{FormatIncorrectMessage} gender");
            student.Gender = gender;

            var isNumber = int.TryParse(streamList[++position], out var age);
            if (!isNumber) throw new FormatException($"{FormatIncorrectMessage} age");
            student.Age = age;

            student.Education = streamList[++position];

            isNumber = int.TryParse(streamList[++position], out var academicYear);
            if (!isNumber) throw new FormatException($"{FormatIncorrectMessage} academic year");
            student.AcademicYear = academicYear;
            return student;
        }

        private static bool IsGenderFormat(char character)
        {
            var upperInvariant = character.ToString().ToUpperInvariant();
            return upperInvariant.Equals("F") || upperInvariant.Equals("M");
        }

    }
}
