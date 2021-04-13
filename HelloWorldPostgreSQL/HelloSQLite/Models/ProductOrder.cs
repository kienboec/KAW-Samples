using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSQLite
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}