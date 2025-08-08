namespace People.Service.Models
{
    public class PersonEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
