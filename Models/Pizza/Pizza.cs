using System;
using System.Collections.Generic;
using System.Text;

namespace AISample.Models.Pizza
{
    public record Pizza
    {
        public PizzaType PizzaType { get; set; }
        public Size Size { get; set; }

        public List<Toppings> Toppings { get; set; }

        public int Quantity { get; set; } = 0;
        public string SpecialInstructions { get; set; } = string.Empty;
    }
}
