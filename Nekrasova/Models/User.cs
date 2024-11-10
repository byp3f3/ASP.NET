namespace Nekrasova.Models
{
    public record class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate {  get; set; }
        public string PhoneNumber { get; set; }
        public int LogPass_ID { get; set; }
    }
}
