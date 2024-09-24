﻿using Day14_PizzaRepository.Models;

namespace Day14_PizzaRepository.Interface
{
    public interface IRepository<K, T> where T : class
    {
        T Add(T item);
        T Get(K key);
        List<T> GetAll();
        T Update(T Item);
        T Delete(K key);
    }
}
