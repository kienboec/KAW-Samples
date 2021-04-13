using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace HelloSQLite
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(int productId, string name, double price)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Price = price;
        }
    }
}