using SerkoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerkoWebAPI.Repository
{
    public interface IExpenseRepository
    {
        void SaveExpenseClaim(ExpenseClaimModel model);
    }
}
