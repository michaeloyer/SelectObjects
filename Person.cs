namespace SelectObjects
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
    }
}
