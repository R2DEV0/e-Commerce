using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId{get; set;}


        [Required]
        public int? ProductId{get; set;}


        public Product Product{get; set;}


        [Required(ErrorMessage="Customer name is required")]
        public int? CustomerId{get; set;}


        public Customer Customer{get; set;}


        [Required]
        public int? Count{get; set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}