using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerkoWebAPI.Models
{
    public class ExpenseClaimResponse
    {
        public ExpenseClaimModel Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}