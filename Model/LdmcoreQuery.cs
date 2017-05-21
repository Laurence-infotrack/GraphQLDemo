using GraphQL.Types;
using GraphQLDemo.Data;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLDemo.Model
{
    public class LdmcoreQuery : ObjectGraphType
    {

        public LdmcoreQuery(LdmcoreContext dbContext)
        {
            Field<LoginType>(
              "login",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");

                  return dbContext.Logins.FirstOrDefault(x => x.LoginId == id);
              }
            );

            Field<OrderType>(
              "order",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");

                return dbContext.Orders.Find(id);
              }
            );

            Field<ServiceType>(
              "service",
              arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
              resolve: context =>
              {
                  var id = context.GetArgument<int>("id");

                  return dbContext.Services.Find(id);
              }
            );
        }
    }

}