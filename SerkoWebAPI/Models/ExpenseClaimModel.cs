using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerkoWebAPI.Models
{
    public class ExpenseClaimModel
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
    }
}