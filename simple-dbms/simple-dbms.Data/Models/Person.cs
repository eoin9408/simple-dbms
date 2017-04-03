namespace simple_dbms.Data.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public Person(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}";
        }
    }
}