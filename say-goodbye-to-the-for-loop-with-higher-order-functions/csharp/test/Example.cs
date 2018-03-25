using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;

// cd test/; dotnet watch test
namespace test
{
    public class Example
    {
        IImmutableList<(int zip, double price, int quantity)> orders =
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
                    if (orders[i].zip == 53202)
                        total += orders[i].price * orders[i].quantity;     
                }
                
                return total;
            }
            
            double ForEachLoop()
            {
                var total = 0.0;
                foreach (var order in orders)
                {
                    if (order.zip == 53202)
                        total += order.price * order.quantity;
                }

                return total;
            }
            
            double HigherOrderFunctions()
            {
                var total = orders
                    .Where(order => order.zip == 53202)
                    .Select(order => order.price * order.quantity)
                    .Aggregate(0.0, (sub, amount) => sub + amount);
                  
                return total;
            }
        }
        
        [Fact]
        public void Map()
        {
            Assert.Equal(
                Map(order => order.price * order.quantity, orders),
                orders.Select(order => order.price * order.quantity)
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
                Filter(order => order.zip == 53202, orders),
                orders.Where(order => order.zip == 53202)
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
                Fold((sub, order) => sub + order.price, 0.0, orders),
                orders.Aggregate(0.0, (sub, order) => sub + order.price)
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
    }
}
