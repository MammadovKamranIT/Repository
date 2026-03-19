using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.DTO.Order
{
    public class OrderCreateDto
    {

      
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        



    }
}
