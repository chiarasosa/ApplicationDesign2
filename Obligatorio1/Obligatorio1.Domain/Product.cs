﻿using System;
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

        public int Brand { get; set; }

        public int Category { get; set;}

        public int Colors { get; set; }
    
        public Product()
        {
            this.ProductID = 0;
            this.Name = string.Empty;
            this.Price = 0;
            this.Description = string.Empty;
            this.Brand = 0;
            this.Category = 0;
            this.Colors = 0;
        }

        public Product(string name, int price, string description, int brand, int category, int colors)
        {

        }
    
    }




}