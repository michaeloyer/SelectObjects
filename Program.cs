using System;
using System.Linq;

namespace SelectObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintSelectPeople("Select People by First Name", p => p.FirstName);
            PrintSelectPeople("Select People by Last Name", p => p.LastName);
            PrintSelectPeople("Select People by Full Name", p => $"{p.FirstName} {p.LastName}");
            Console.ReadKey();
        }

        static void PrintSelectPeople(string Instruction, Func<Person, string> printer)
        {
            var people = new []
            {
                new Person("George", "Washington"),
                new Person("Mark", "Zuckerberg"),
                new Person("Keira", "Knightly"),
                new Person("Michael", "Smith"),
                new Person("George", "Washington"),
            };

            using (var form = new SelectItems<Person>(Instruction, "Select People", people, p => new[] { p.FirstName, p.LastName }, true))
            {
                form.ShowDialog();
                Console.WriteLine(Instruction + ":");
                Console.WriteLine(string.Join("|", form.Result.Select(printer)));
                Console.WriteLine();
            }
        }
    }
}
