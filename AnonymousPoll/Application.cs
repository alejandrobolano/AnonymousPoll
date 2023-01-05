using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using AnonymousPoll.Business.Contracts;
using AnonymousPoll.Common;
using AnonymousPoll.Common.Models;
using log4net;

namespace AnonymousPoll
{
    internal class Application
    {
        private readonly IBusinessLogic _businessLogic;
        public static string SolutionName = "Anonymous Poll";
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public Application(IBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        public void Run()
        {
            DoStartForm();
        }

        private static void ShowSolutionName()
        {
            Console.WriteLine("\r\n     _                                                        ____       _ _ \r\n    / \\   _ __   ___  _ __  _   _ _ __ ___   ___  _   _ ___  |  _ \\ ___ | | |\r\n   / _ \\ | '_ \\ / _ \\| '_ \\| | | | '_ ` _ \\ / _ \\| | | / __| | |_) / _ \\| | |\r\n  / ___ \\| | | | (_) | | | | |_| | | | | | | (_) | |_| \\__ \\ |  __| (_) | | |\r\n /_/   \\_|_| |_|\\___/|_| |_|\\__, |_| |_| |_|\\___/ \\__,_|___/ |_|   \\___/|_|_|\r\n                            |___/                                            \r\n");
            Console.WriteLine();
        }

        private void DoStartForm()
        {
            while (true)
            {
                ShowSolutionName();
                Console.WriteLine("Tell us the quantity cases to analyze");
                var quantity = GetQuantityCases();
                var hasError = false;
                var partialStudents = new Dictionary<int, Student>();
                try
                {
                    var cases = DoAskEachCases(quantity);
                    partialStudents = _businessLogic.GetPartialStudentsConverted(cases);
                }
                catch (Exception)
                {
                    hasError = true;
                }

                if (!hasError)
                {
                    Console.WriteLine("\nResult:");
                    WriteAnswers(partialStudents);
                }

                Console.WriteLine("\nDo you want repeat? y/n");
                var answer = Console.ReadLine();
                if (!string.IsNullOrEmpty(answer) && answer.Equals("y"))
                {
                    Console.Clear();
                    continue;
                }
                break;
            }
        }

        private static List<string> DoAskEachCases(int quantity)
        {
            var result = new List<string>();
            Console.WriteLine("\nFormat will be: 'gender,age,studies,academic year'\n");
            for (var index = 0; index < quantity; index++)
            {
                var caseToAnalyze = GetEachCase(index);
                result.Add(caseToAnalyze);
            }

            return result;
        }
        private static string GetEachCase(int index)
        {
            while (true)
            {
                Console.WriteLine($"Case #{index + 1} to analyze: ");
                var caseToAnalyze = Console.ReadLine();
                if (string.IsNullOrEmpty(caseToAnalyze)) continue;
                return caseToAnalyze;
            }
        }
        private static int GetQuantityCases()
        {
            while (true)
            {
                var possibleNumber = Console.ReadLine();
                var isNumber = int.TryParse(possibleNumber, out var number);
                if (isNumber)
                {
                    if (number <= 100) return number;
                    Log.Warn("Sorry, but the number can't be upper than 100!");
                    continue;
                }
                Log.Warn("Type only numbers. Repeat, please!");
            }
        }
        private void WriteAnswers(Dictionary<int, Student> partialStudents)
        {
            foreach (var partialStudent in partialStudents)
            {
                try
                {
                    var nameOfStudentsIdentified = _businessLogic.GetNameOfStudentsIdentified(partialStudent.Value);
                    WriteAnswer(partialStudent.Key, nameOfStudentsIdentified);
                }
                catch (Exception)
                {
                    break;
                }
                
            }
        }

        private void WriteAnswer(int occurrence, List<string> names)
        {
            var result = _businessLogic.GetNamesAppended(names);
            Log.Info(($"Case #{occurrence}: {result}"));
        }
    }
}
