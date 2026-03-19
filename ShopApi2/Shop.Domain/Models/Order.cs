namespace Shop.Models
{
    public class Order


    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? Product { get; set; }

        public decimal TotalSum { get; set; }

        public Customer customer { get; set; } = null!;



    }

}
