using GraphQL.Types;
using GraphQLDemo.Data;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Model
{
    public class LdmcoreQuery : ObjectGraphType
    {

        public LdmcoreQuery(LdmcoreContext dbContext)
        {
            Field<LoginType>(
              "Login",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  return dbContext.Logins.FirstOrDefault(x => x.LoginId == id);
              }
            );

            Field<OrderType>(
              "Order",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                var id = context.GetArgument<int>("id");
                //var includeFields = context.ParentType.Fields.Where(x => x.Name != context.FieldName);
                //dbContext.Orders.Include(x => x.)
                var order = dbContext.Orders.Include("service").Include("OrderedBy").FirstOrDefault(x => x.Id == id);
                return order;
              }
            );

            
            Field<ListGraphType<OrderType>>(
              "Orders",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int?>("id");
                  if (id.HasValue)
                  {
                      var order = dbContext.Orders.FirstOrDefault(x => x.Id == id);
                      return new List<Order>() { order };
                  }

                  return dbContext.Orders;
              }
            );

            Field<ServiceType>(
              "Service",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");
                  
                  // TODO:: Find the proper way to do this
                  
                  return dbContext.Services.Find(id);
              }
            );
        }
    }

}