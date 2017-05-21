using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.Data
{
    public static class TestData
    {
        public static void AddTestData(LdmcoreContext context)
        {
            var service = new Service()
            {
                ServiceId = 1,
                Name = "Nsw Title Search"
            };

            var login = new Login()
            {
                LoginId = 1,
                Name = "John Smith"
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    ServiceId = 1,
                    LoginId = 1,
                    Fee = 5.00m
                },
                
                new Order()
                {
                    Id = 2,
                    ServiceId = 1,
                    LoginId = 1,
                    Fee =75.00m
                }


            };

            context.Services.Add(service);
            context.Logins.Add(login);
            context.Orders.AddRange(orders);


            context.SaveChanges();
        }
    }
}
