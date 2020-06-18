using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId{get; set;}


        [Required(ErrorMessage="Product name is required")]
        [MinLength(3, ErrorMessage="Name is must be at least 3 characters long")]
        public string Name{get; set;}


        [Required(ErrorMessage="An image is required")]
        public string ImageUrl{get; set;}


        [Required(ErrorMessage="A description is required")]
        public string Description{get; set;}

        [Required]
        public int? Count{get; set;}


        public List<Order> OrderedOrders{get;set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}