using System;
using System.Collections.Generic;
using System.Text;

namespace AISample.Models.Pizza
{
    public record Order
    {
        public int OrderId { get; set; }
        public List<Pizza> Pizza { get; set; }
        public string OtherStuff { get; set; } = string.Empty;

    }
}
