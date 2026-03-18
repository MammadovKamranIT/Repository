namespace Shop.DTO.Order_DTOs
{
    public class OrderCreateRequest
    {



        public string Title { get; set; } = string.Empty;
       
        public string Description { get; set; } = string.Empty;
       
        public int CustomerId { get; set; }


    }
}
