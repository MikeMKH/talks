using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;

// cd test/; dotnet watch test
namespace test
{
    public static class ExampleExt
    {
        public static void Iterate<T>(
            this IEnumerator<T> source, Action<T> f)
        {
            while(source.MoveNext())
            {
                f(source.Current);
            }
        }
        
        public static IEnumerable<U> Map<T, U>(
            this IEnumerable<T> source, Func<T, U> mapping)
        {
            var result = new List<U>();
            foreach(var item in source)
            {
                result.Add(mapping(item));
            }
            
            return result;
        }
        
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var result = new List<T>();
            foreach(var item in source)
            {
                if (predicate(item))
                result.Add(item);
            }
            
            return result;
        }
        
        public static U Fold<T, U>(
            this IEnumerable<T> source,
            U initial,
            Func<U, T, U> accumulate)
        {
            var result = initial;
            foreach(var item in source)
            {
                result = accumulate(result, item);
            }
            
            return result;
        }
    }
    
    public class ExampleTests
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
            
            orders.GetEnumerator()
              .Iterate(order => result.Add(order));
            
            Assert.Equal(orders, result);
        }
        
        [Fact]
        public void Map()
        {
            Assert.Equal(
                orders.Map(order => order.Price * order.Quantity),
                orders.Select(order => order.Price * order.Quantity)
            );
        }
        
        [Fact]
        public void Filter()
        {
            Assert.Equal(
                orders.Filter(order => order.Zip == 53202),
                orders.Where(order => order.Zip == 53202)
            );
        }
        
        [Fact]
        public void Fold()
        {
            Assert.Equal(
                orders.Fold(0.0, (sub, order) => sub + order.Price),
                orders.Aggregate(0.0, (sub, order) => sub + order.Price)
            );
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
        
        [Fact]
        public void ListComprehension()
        {
            var comprehension = 
              (from order in orders
               where order.Zip == 53202
               select new {Amount = order.Price * order.Quantity})
              .Sum(order => order.Amount);
              
            var fluent =
              orders
                .Where(order => order.Zip == 53202)
                .Select(order => order.Price * order.Quantity)
                .Sum(amount => amount);
                         
            Assert.Equal(comprehension, fluent);
        }
    }
}
