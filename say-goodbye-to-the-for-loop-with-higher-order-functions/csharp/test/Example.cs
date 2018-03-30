using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;

// cd test/; dotnet watch test
namespace test
{
    public class Example
    {
        IImmutableList<(int Zip, double Price, int Quantity)> orders =
            ImmutableList.Create(
                (53202, 1.89, 3),
                (60191, 1.99, 2),
                (60060, 0.99, 7),
                (53202, 1.29, 8),
                (60191, 1.89, 2),
                (53202, 0.99, 3)
        );
        
        [Fact]
        public void Realistic()
        {   
            Assert.Equal(ForLoop(), ForEachLoop());
            Assert.Equal(ForLoop(), HigherOrderFunctions());
            
            double ForLoop()
            {   
                var total = 0.0;
                for (int i = 0; i < orders.Count(); i++)
                {
                    if (orders[i].Zip == 53202)
                        total += orders[i].Price * orders[i].Quantity;     
                }
                
                return total;
            }
            
            double ForEachLoop()
            {
                var total = 0.0;
                foreach (var order in orders)
                {
                    if (order.Zip == 53202)
                        total += order.Price * order.Quantity;
                }

                return total;
            }
            
            double HigherOrderFunctions()
            {
                var total = orders
                    .Where(order => order.Zip == 53202)
                    .Select(order => order.Price * order.Quantity)
                    .Aggregate(0.0, (sub, amount) => sub + amount);
                  
                return total;
            }
        }
        
        [Fact]
        public void IteratorPattern()
        {
            var result = new List<(int Zip, double Price, int Quantity)>();
            
            Iterate(order => result.Add(order), orders.GetEnumerator());
            
            Assert.Equal(orders, result);
            
            void Iterate<T>(Action<T> f, IEnumerator<T> source)
            {
                while(source.MoveNext())
                {
                    f(source.Current);
                }
            }
        }
        
        [Fact]
        public void Map()
        {
            Assert.Equal(
                Map(order => order.Price * order.Quantity, orders),
                orders.Select(order => order.Price * order.Quantity)
            );
            
            IEnumerable<U> Map<T, U>(
                Func<T, U> mapping, IEnumerable<T> source)
            {
                var result = new List<U>();
                foreach(var item in source)
                {
                    result.Add(mapping(item));
                }
                
                return result;
            }
        }
        
        [Fact]
        public void Filter()
        {
            Assert.Equal(
                Filter(order => order.Zip == 53202, orders),
                orders.Where(order => order.Zip == 53202)
            );
            
            IEnumerable<T> Filter<T>(
                Func<T, bool> predicate, IEnumerable<T> source)
            {
                var result = new List<T>();
                foreach(var item in source)
                {
                    if (predicate(item))
                    result.Add(item);
                }
                
                return result;
            }
        }
        
        [Fact]
        public void Fold()
        {
            Assert.Equal(
                Fold((sub, order) => sub + order.Price, 0.0, orders),
                orders.Aggregate(0.0, (sub, order) => sub + order.Price)
            );
            
            U Fold<T, U>(
                Func<U, T, U> accumulate, U initial,
                IEnumerable<T> source)
            {
                var result = initial;
                foreach(var item in source)
                {
                    result = accumulate(result, item);
                }
                
                return result;
            }
        }
        
        [Fact]
        public void SpyOnHigherOrderFunctions()
        {
            var spy = new List<string>();

            orders
                .Where(order => { spy.Add("filter"); return order.Zip == 53202; })
                .Select(order => { spy.Add("map"); return order.Price * order.Quantity; })
                .Aggregate(0.0, (sub, amount) => { spy.Add("fold"); return sub + amount; });
                
            Assert.Equal(
                new List<string> {
                    "filter", "map", "fold",
                    "filter",
                    "filter",
                    "filter", "map", "fold",
                    "filter",
                    "filter", "map", "fold"
                },
                spy);
        }
    }
}
