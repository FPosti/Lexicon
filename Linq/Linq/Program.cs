using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqOvningar
{
    public class Student
    {
        public int Id { get; set; }
        public string Namn { get; set; } = "";
        public int Alder { get; set; }
        public string Klass { get; set; } = "";
        public int Poang { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> studenter = new List<Student>
            {
                new Student { Id = 1, Namn = "Anna", Alder = 19, Klass = "A", Poang = 85 },
                new Student { Id = 2, Namn = "Bertil", Alder = 22, Klass = "B", Poang = 72 },
                new Student { Id = 3, Namn = "Cecilia", Alder = 20, Klass = "A", Poang = 91 },
                new Student { Id = 4, Namn = "David", Alder = 24, Klass = "C", Poang = 64 },
                new Student { Id = 5, Namn = "Eva", Alder = 19, Klass = "B", Poang = 85 },
                new Student { Id = 6, Namn = "Felix", Alder = 21, Klass = "A", Poang = 77 },
                new Student { Id = 7, Namn = "Gustav", Alder = 23, Klass = "C", Poang = 58 },
                new Student { Id = 8, Namn = "Hanna", Alder = 20, Klass = "B", Poang = 95 }
            };

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("LINQ-ÖVNINGAR MED STUDENTER\n");

            // 1. Hämta alla studenter som är äldre än 20.
            var olderThan20 = studenter.Where(student => student.Alder > 20);
            PrintStudents("1. Studenter äldre än 20 år", olderThan20);

            // 2. Hämta alla studenter som har mer än 80 poäng.
            var moreThan80Points = studenter.Where(student => student.Poang > 80);
            PrintStudents("2. Studenter med mer än 80 poäng", moreThan80Points);

            // 3. Visa endast namnen på alla studenter.
            var names = studenter.Select(student => student.Namn);
            Console.WriteLine("3. Alla namn");
            Console.WriteLine(string.Join(", ", names));
            Console.WriteLine();

            // 4. Visa namn och poäng i en ny anonym typ.
            var namesAndPoints = studenter.Select(student => new { student.Namn, student.Poang });
            Console.WriteLine("4. Namn och poäng");
            foreach (var student in namesAndPoints)
            {
                Console.WriteLine($"{student.Namn}: {student.Poang} poäng");
            }
            Console.WriteLine();

            // 5. Sortera studenter efter ålder, lägst först.
            var sortedByAge = studenter.OrderBy(student => student.Alder);
            PrintStudents("5. Sorterade efter ålder", sortedByAge);

            // 6. Sortera studenter efter poäng, högst först.
            var sortedByPoints = studenter.OrderByDescending(student => student.Poang);
            PrintStudents("6. Sorterade efter poäng, högst först", sortedByPoints);

            // 7. Räkna hur många studenter som finns.
            int studentCount = studenter.Count;
            Console.WriteLine($"7. Antal studenter: {studentCount}");

            // 8. Räkna hur många som går i klass A.
            int classACount = studenter.Count(student => student.Klass == "A");
            Console.WriteLine($"8. Antal i klass A: {classACount}");
            Console.WriteLine();

            // 9. Ta ut de tre första studenterna i listans ursprungliga ordning.
            var firstThree = studenter.Take(3);
            PrintStudents("9. De tre första studenterna", firstThree);

            // 10. Ta ut de två bästa poängen efter sortering.
            var topTwoPoints = studenter
                .OrderByDescending(student => student.Poang)
                .Take(2)
                .Select(student => student.Poang);
            Console.WriteLine("10. De två bästa poängen");
            Console.WriteLine(string.Join(", ", topTwoPoints));
            Console.WriteLine();

            // 11. Visa alla unika klassnamn.
            var uniqueClasses = studenter.Select(student => student.Klass).Distinct();
            Console.WriteLine("11. Unika klassnamn");
            Console.WriteLine(string.Join(", ", uniqueClasses));

            // 12. Visa alla unika åldrar.
            var uniqueAges = studenter.Select(student => student.Alder).Distinct();
            Console.WriteLine("12. Unika åldrar");
            Console.WriteLine(string.Join(", ", uniqueAges));
            Console.WriteLine();

            // 13. Finns det någon student med exakt 100 poäng?
            bool has100Points = studenter.Any(student => student.Poang == 100);
            Console.WriteLine($"13. Någon med 100 poäng: {(has100Points ? "Ja" : "Nej")}");

            // 14. Har alla studenter minst 50 poäng?
            bool allHaveAtLeast50Points = studenter.All(student => student.Poang >= 50);
            Console.WriteLine($"14. Alla har minst 50 poäng: {(allHaveAtLeast50Points ? "Ja" : "Nej")}");
            Console.WriteLine();

            // 15. Hämta första studenten i klass B.
            Student? firstInClassB = studenter.FirstOrDefault(student => student.Klass == "B");
            Console.WriteLine("15. Första studenten i klass B");
            if (firstInClassB is not null)
            {
                PrintStudent(firstInClassB);
            }
            else
            {
                Console.WriteLine("Ingen student finns i klass B.");
            }
            Console.WriteLine();

            // 16. Hämta sista studenten i listan.
            Student lastStudent = studenter.Last();
            Console.WriteLine("16. Sista studenten i listan");
            PrintStudent(lastStudent);
            Console.WriteLine();

            // 17. Hämta studenten med Id 3.
            int selectedId = 3;
            Student studentById = studenter.Single(student => student.Id == selectedId);
            Console.WriteLine($"17. Studenten med Id {selectedId}");
            PrintStudent(studentById);
            Console.WriteLine();

            // 18. Gruppera studenter per klass.
            var groupsByClass = studenter.GroupBy(student => student.Klass);

            // 19. Skriv ut varje klass och namnen i gruppen.
            Console.WriteLine("18-19. Studenter grupperade per klass");
            foreach (var group in groupsByClass)
            {
                Console.WriteLine($"Klass {group.Key}: {string.Join(", ", group.Select(student => student.Namn))}");
            }
            Console.WriteLine();

            // 20. Summera alla poäng.
            int totalPoints = studenter.Sum(student => student.Poang);
            Console.WriteLine($"20. Summa poäng: {totalPoints}");

            // 21. Räkna ut medelpoäng.
            double averagePoints = studenter.Average(student => student.Poang);
            Console.WriteLine($"21. Medelpoäng: {averagePoints:0.###}");

            // 22. Hitta lägsta och högsta poäng.
            int lowestPoints = studenter.Min(student => student.Poang);
            int highestPoints = studenter.Max(student => student.Poang);
            Console.WriteLine($"22. Lägsta poäng: {lowestPoints}, högsta poäng: {highestPoints}");
        }

        private static void PrintStudents(string title, IEnumerable<Student> students)
        {
            Console.WriteLine(title);
            foreach (Student student in students)
            {
                PrintStudent(student);
            }
            Console.WriteLine();
        }

        private static void PrintStudent(Student student)
        {
            Console.WriteLine($"Id {student.Id}: {student.Namn}, {student.Alder} år, klass {student.Klass}, {student.Poang} poäng");
        }
    }
}
