﻿using Day14_Login.Models;
using Day14_Login.Exceptions;

namespace Day14_Login.Repositories
{
    public class PizzaRepository : IRepository<int, Pizza>
    {
        List<Pizza> pizzas = new List<Pizza>()
        {
            new Pizza(){Id = 1, Name = "Margherita", Description = "Cheese and Tomato", Price = 5.99f, Image = "margherita.jpeg"},
            new Pizza(){Id = 2, Name = "Pepperoni", Description = "Pepperoni and Cheese", Price = 7.99f, Image = "download.jpeg"}
        };
        public PizzaRepository()
        {
        }
        public Pizza Add(Pizza pizza)
        {
            pizzas.Add(pizza);
            return pizza;
        }

        public Pizza Delete(int id)
        {
            var pizza = Get(id);
            pizzas.Remove(pizza);
            return pizza;
        }

        public Pizza Get(int id)
        {
            var pizza = pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza == null)
                throw new NoSuchPizzaException();
            return pizza;
        }

        public List<Pizza> GetAll()
        {
            if (pizzas.Count == 0)
                throw new NoPizzasException();
            return pizzas;
        }

        public Pizza Update(Pizza pizza)
        {
            var myPizza = Get(pizza.Id);
            if (myPizza == null)
                throw new NoSuchPizzaException();
            myPizza.Name = pizza.Name;
            myPizza.Description = pizza.Description;
            myPizza.Price = pizza.Price;
            myPizza.Image = pizza.Image;
            return myPizza;
        }
    }
}
