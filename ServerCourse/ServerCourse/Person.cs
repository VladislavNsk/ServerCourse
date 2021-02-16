namespace Excel
{
    public class Person
    {
        public string FirstName { get; }

        public string LastName { get; }

        public int Age { get; }

        public string Phone { get; }

        public Person(string firstName, string lastName, int age, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Phone = phone;
        }
    }
}