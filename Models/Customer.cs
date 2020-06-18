using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId {get; set;}
    
        [Required(ErrorMessage="Customer name is required")]
        [MinLength(2, ErrorMessage="Customer name must be at least 2 characters")]
        public string Name {get; set;}

        public List<Order> OrderedOrders {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}