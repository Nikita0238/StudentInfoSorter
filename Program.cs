using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentSessionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            string outputFile = "output.txt";

            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Файл input.txt не найден.");
                return;
            }

            List<Student> students = new List<Student>();

            // FullName;YearOfBirth;Address;GroupNumber;ExamResult1,ExamResult2,...
            var lines = File.ReadAllLines(inputFile);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length < 5)
                {
                    Console.WriteLine($"Неверный формат строки: {line}");
                    continue;
                }

                try
                {
                    string fullName = parts[0];
                    int yearOfBirth = int.Parse(parts[1]);
                    string address = parts[2];
                    int groupNumber = int.Parse(parts[3]);
                    int[] examResults = parts[4]
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => int.Parse(r.Trim()))
                        .ToArray();

                    Student student = new Student(fullName, yearOfBirth, address, groupNumber, examResults);
                    students.Add(student);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при обработке строки: {line}. Ошибка: {ex.Message}");
                }
            }

            var passedStudents = students.Where(s => s.PassedSession()).ToList();

            var sortedByGroup = passedStudents.OrderBy(s => s.GroupNumber).ToList();

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine("Студенты, успешно сдавшие сессию, отсортированные по номеру группы:");
                foreach (var student in sortedByGroup)
                {
                    writer.WriteLine(student.ToString());
                }
            }

            Console.WriteLine($"Результаты записаны в {outputFile}");
        }
    }
}
