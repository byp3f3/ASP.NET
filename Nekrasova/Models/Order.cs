namespace Nekrasova.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public DateOnly OrderDate { get; set; }
        public double FinalCost { get; set; }
        public int FinalWeight { get; set; }
    }
}
