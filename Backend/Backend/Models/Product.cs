using System;
using System.Collections.Generic;

namespace OrderManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public string Material { get; set; }
    }
}
