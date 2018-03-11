using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public class TransactionStepNameAttribute : ActionFilterAttribute
    {
        public string Step { get; set; }
        public TransactionStepNameAttribute(string step) {
            this.Step = step;
        }
    }
}
