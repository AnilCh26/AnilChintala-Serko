using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SerkoWebAPI.Models;

namespace SerkoWebAPI.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        public   void SaveExpenseClaim(ExpenseClaimModel model)
        {
          //ToDo Save to DB or Pass to Client
        }
    }
}