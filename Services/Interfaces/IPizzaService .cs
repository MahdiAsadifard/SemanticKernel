using AISample.Models.Pizza;
using System;
using System.Collections.Generic;
using System.Text;

namespace AISample.Services.Interfaces
{
    public interface IPizzaService
    {
        Task<Menu> GetMenu();
        Task<Order> GetOrder(int orderId);

        Task<Order> AddPizzaToOrder(Size size, List<Toppings> toppings, int quantity = 1, string specialInstructions = "");
        Task<Pizza> GetPizzaFromCart(int orderId, PizzaType pizzaType);
        Task<Order> RemovePizzaFromOrder(int orderId, PizzaType pizzaType);
    }
}
