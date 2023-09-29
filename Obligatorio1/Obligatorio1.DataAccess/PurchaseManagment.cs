﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio1.IDataAccess;
using Obligatorio1.Domain;

namespace Obligatorio1.DataAccess
{
    public class PurchaseManagment : IPurchaseManagment
    {
        
        public int ValidateMoreThan1Item(Cart cart)
        {
            
            return cart.Products.Count;
        }
    }
}
