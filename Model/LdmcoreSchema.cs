using GraphQL.Types;
using System;

namespace GraphQLDemo.Model
{
    public class LdmcoreSchema : Schema
    {
        public LdmcoreSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (LdmcoreQuery)resolveType(typeof(LdmcoreQuery));
        }
    }
}
