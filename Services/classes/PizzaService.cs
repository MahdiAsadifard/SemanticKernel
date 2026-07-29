using AISample.Models.Pizza;
using AISample.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace AISample.Services.classes
{
    public class PizzaService : IPizzaService
    {
        private readonly Order _orders;

        public PizzaService()
        {
            _orders = new Order();
        }

        public Task<Menu> GetMenu()
        {
            return Task.FromResult(new Menu());
        }
        public async Task<Order> GetOrder(int cartId)
        {
            return await Task.FromResult(_orders);
        }

        public Task<Order> AddPizzaToOrder(Size size, List<Toppings> toppings, int quantity = 1, string specialInstructions = "")
        {
            _orders.Pizza.Add(new Pizza
            {
                Size = size,
                Toppings = toppings,
                Quantity = quantity,
                SpecialInstructions = specialInstructions
            });
            return Task.FromResult(_orders);
        }

        public async Task<Pizza> GetPizzaFromCart(int orderId, PizzaType pizzaType)
        {
            if (_orders.OrderId != orderId) throw new InvalidOperationException("Order ID does not match.");
            var pizza = _orders.Pizza.Find(p => p.PizzaType == pizzaType);
            if (pizza == null) throw new InvalidOperationException("Pizza not found in the order.");
            return await Task.FromResult(pizza);
        }

        public Task<Order> RemovePizzaFromOrder(int orderId, PizzaType pizzaType)
        {
            if (_orders.OrderId != orderId)
            {
                throw new InvalidOperationException("Order ID does not match.");
            }
            _orders.Pizza.RemoveAll(p => p.PizzaType == pizzaType);
            return Task.FromResult(_orders);
        }
    }
}
