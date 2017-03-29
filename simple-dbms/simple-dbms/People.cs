namespace simple_dbms
{
    public class People
    {
        public int id { get; set; }
        public string name { get; set; }

        public People() { }

        public People(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public override string ToString()
        {
            return $"{this.id}, {this.name}";
        }
    }
}