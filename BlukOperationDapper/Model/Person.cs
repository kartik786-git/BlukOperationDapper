namespace BlukOperationDapper.Model
{
    public class Person
    {
        public int Id { get; set; }        // The ID is auto-generated in the database (Identity)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
