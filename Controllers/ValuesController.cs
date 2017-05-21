using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GraphQLDemo.Data;
using GraphQLDemo.Model;
using GraphQL.Types;
using GraphQL;

namespace GraphQLDemo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        LdmcoreContext _dbContext;
        ISchema _schema;
        IDocumentExecuter _documentExecuter;
        public ValuesController(LdmcoreContext dbContext, IDocumentExecuter documentExecuter, ISchema schema)
        {
            _dbContext = dbContext;
            _schema = schema;
            _documentExecuter = documentExecuter;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var executionOptions = new ExecutionOptions { Schema = _schema, Query = query.Query };

            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
