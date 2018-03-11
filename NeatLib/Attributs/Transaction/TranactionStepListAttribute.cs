using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public class TranactionStepListAttribute : ActionFilterAttribute
    {
        public IList<TransactionStep> StepList { get; private set; } 
        public TranactionStepListAttribute(Type transactionStepSetter) {
            var setter = Activator.CreateInstance(transactionStepSetter) as ITransactionStepSetter;
            if (setter == null)
                throw new ArgumentException("transactionStepSetter",new Exception("transactionStepSetter must be of type ITransactionStepSetter"));
            StepList = setter.Steps;
        }
    }
}
