using SerkoWebAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SerkoWebAPI.Controllers
{
    public class ExpenseController : ApiController
    {
        private readonly IExpenseProcessService _expenseProcessService;

        public ExpenseController(IExpenseProcessService expenseProcessService)
        {
            _expenseProcessService = expenseProcessService ?? throw new ArgumentNullException(nameof(expenseProcessService));
        }
        public async Task<IHttpActionResult> Post([FromBody] string text)
        {
            return Ok(await _expenseProcessService.CreateExpenseClaim(text));
        }
    }
}
