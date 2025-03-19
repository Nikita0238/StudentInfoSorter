using System;
using System.Linq;

namespace StudentSessionProject
{
    public class Student : IComparable<Student>
    {
        public string FullName { get; set; }
        public int YearOfBirth { get; set; }
        public string Address { get; set; }
        public int GroupNumber { get; set; }
        public int[] ExamResults { get; set; }

        public Student(string fullName, int yearOfBirth, string address, int groupNumber, int[] examResults)
        {
            FullName = fullName;
            YearOfBirth = yearOfBirth;
            Address = address;
            GroupNumber = groupNumber;
            ExamResults = examResults;
        }

        // Сортировка по году рождения (используется для CompareTo)
        public int CompareTo(Student other)
        {
            if (other == null) return 1;
            return this.YearOfBirth.CompareTo(other.YearOfBirth);
        }

        // Переопределение ToString для удобного вывода информации о студенте
        public override string ToString()
        {
            string examResultsStr = string.Join(", ", ExamResults);
            return $"ФИО: {FullName}, Год рождения: {YearOfBirth}, Адрес: {Address}, Группа: {GroupNumber}, Результаты экзаменов: [{examResultsStr}]";
        }

        // Метод для проверки, сдал ли студент сессию (все оценки >= 3)
        public bool PassedSession()
        {
            return ExamResults.All(result => result >= 3);
        }
    }
}
