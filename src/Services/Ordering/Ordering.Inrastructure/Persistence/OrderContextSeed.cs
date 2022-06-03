using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Seed database associated with context {typeof(OrderContext).Name}");
            }
        }

        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    UserName = "swn",
                    FirstName = "Stefan",
                    LastName = "Hafner",
                    EmailAddress = "stefan.hafner.development@gmail.com",
                    AddressLine = "Testaddresse 1",
                    Country = "Deutschland",
                    TotalPrice = 350
                }
            };
        }
    }
}
