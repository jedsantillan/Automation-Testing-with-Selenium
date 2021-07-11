using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PlanitAssessment_JohnEdwardSantillan.Models
{
    public class CartItemModel
    {
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
    }
}
