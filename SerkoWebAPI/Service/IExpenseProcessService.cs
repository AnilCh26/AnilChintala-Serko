using SerkoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerkoWebAPI.Service
{
  public  interface IExpenseProcessService
    {
        Task<ExpenseClaimResponse> CreateExpenseClaim(string text);
    }
}
