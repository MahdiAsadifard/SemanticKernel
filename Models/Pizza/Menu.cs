using System;
using System.Collections.Generic;
using System.Text;

namespace AISample.Models.Pizza
{
    public record Menu
    {
        public List<string> Items { get; } = new List<string>()
        {
            "Pepperoni",
            "Cheese",
            "Veggie",
            "Meat Lovers",
            "Hawaiian",
            "BBQ Chicken",
            "Supreme",
            "Buffalo Chicken",
            "Margherita",
            "White Pizza",
            "Spinach and Feta",
        };
    }
}
