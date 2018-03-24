using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xunit;

// dotnet watch test
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
    }
}
