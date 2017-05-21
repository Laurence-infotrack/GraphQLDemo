using GraphQL.Types;
using GraphQLDemo.Data;
using System.Linq;

namespace GraphQLDemo.Model
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(LdmcoreContext dbContext)
        {
            Field(x => x.Id).Description("The Id of the Order.");
            Field(x => x.LoginId).Description("Login Id of the user.");
            Field(x => x.ServiceId).Description("Service Id of the service.");
            Field<ServiceType>("Service", "Service object for order", null,
            resolve: context =>
            {
                var serviceId = context.Source.ServiceId;
                return dbContext.Services.Find(serviceId);
            });
            Field<LoginType>("OrderedBy", "Login object for order", null,
            resolve: context =>
            {
                var loginId = context.Source.LoginId;
                return dbContext.Logins.Find(loginId);
            });
        }
    }

    public class ServiceType : ObjectGraphType<Service>
    {
        public ServiceType(LdmcoreContext dbContext)
        {
            Field(x => x.ServiceId).Description("The Id of the service.");
            Field(x => x.Name).Description("Name of the service.");
            Field<ListGraphType<OrderType>>(
                "Orders",
                resolve: context =>
                {
                    var serviceId = context.Source.ServiceId;
                    return dbContext.Orders.Where(x => x.ServiceId == serviceId);
                }
            );

        }
    }

    public class LoginType : ObjectGraphType<Login>
    {
        public LoginType(LdmcoreContext dbContext)
        {
            Field(x => x.LoginId).Description("The Id of the login.");
            Field(x => x.Name).Description("Name of the user.");
            Field<ListGraphType<OrderType>>(
                "Orders",
                resolve: context =>
                {
                    var loginId = context.Source.LoginId;
                    return dbContext.Orders.Where(x => x.LoginId == loginId);
                }
            );
        }
    }
}
