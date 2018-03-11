using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public class TransactionNameAttribute :  ActionFilterAttribute
    {
        public string Name { get; set; }
        public TransactionNameAttribute(string name) {
            this.Name = name;
        }
    }
}
