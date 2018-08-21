using SerkoWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SerkoWebAPI.Repository;

namespace SerkoWebAPI.Service
{
    public class ExpenseProcessService : IExpenseProcessService
    {
        private readonly IExpenseRepository _expenseRepository;
        private const string DEFAULT_COST_CENTRE = "UNKNOWN";
        public ExpenseProcessService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        }
        public Task<ExpenseClaimResponse> CreateExpenseClaim(string text)
        {
            var response = new ExpenseClaimResponse();
            try
            {
                var model = CreateModel(text);
                Save(model);

                response.Success = true;
                response.Result = model;
            }
            catch (ArgumentException argumentException)
            {
                response.Message = argumentException.Message;
            }
            catch (FormatException formatException)
            {
                response.Message = formatException.Message;
            }

            return Task.FromResult(response);
        }

        private ExpenseClaimModel CreateModel(string text)
        {
            text = text.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " "); // remove line breaks from email body

            var model = new ExpenseClaimModel
            {
                CostCentre = GetTagValue(text, "cost_centre") ?? DEFAULT_COST_CENTRE,
                Description = GetTagValue(text, "description"),
                PaymentMethod = GetTagValue(text, "payment_method"),
                Vendor = GetTagValue(text, "vendor"),
                Date = GetDate(text),
                Total = GetTotal(text),
            };

            return model;
        }

        private string GetTagValue(string text, string tagName)
        {
            var openTag = $"<{tagName}>";
            var closeTag = $"</{tagName}>";
            var openIndex = text.IndexOf(openTag) + openTag.Length;
            var closeIndex = text.IndexOf(closeTag);

            var hasMissingTags = openIndex - openTag.Length == -1 && closeIndex == -1;
            if (hasMissingTags)
            {
                return null; // caller can assign a default value or fail the whole call
            }

            var hasOpeningTagWithoutClosingTag = openIndex - openTag.Length > -1 && closeIndex == -1;
            if (hasOpeningTagWithoutClosingTag)
            {
                throw new ArgumentException($"can't find closing tag for {openTag}");
            }

            return text.Substring(openIndex, closeIndex - openIndex);
        }

        private decimal GetTotal(string text)
        {
            var totalString = GetTagValue(text, "total") ?? throw new ArgumentException("missing <total></total> tag");
            return decimal.Parse(totalString);
        }

        private DateTime GetDate(string text)
        {
            var dateString = GetTagValue(text, "date");
            return DateTime.Parse(dateString);
        }
        private void Save(ExpenseClaimModel model)
        {
            //Send Expense Model to Client
            _expenseRepository.SaveExpenseClaim(model);
        }
    }
}