using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSQLite.Dapper
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public Product()
        {
            
        }

        public Product(int productId, string name, double price)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Price = price;
        }
    }
}