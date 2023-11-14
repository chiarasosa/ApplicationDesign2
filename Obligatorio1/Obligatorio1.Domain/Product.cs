using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio1.Domain
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Category { get; set; }
        public string Color { get; set; }

        public bool AvailableToPromotions { get; set; }

        public int Stock { get; set; }
        public Product()
        {
            this.Name = string.Empty;
            this.Price = 0;
            this.Description = string.Empty;
            this.Brand = string.Empty; 
            this.Category = 0;
            this.Color = string.Empty;
            this.Stock = 0;
            this.AvailableToPromotions = true;
        }

        public Product(string name, int price, string description, string brand, int category, string color, int stock, 
            bool availableToPromotions)
        {
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Brand = brand;
            this.Category = category;
            this.Color = color;
            this.Stock = stock;
            this.AvailableToPromotions = availableToPromotions;
        }
    }
}