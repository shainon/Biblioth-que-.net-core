using NeatLib.Attributs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioheque_.net_core.Controllers
{
    public class TransactionStepSetter : ITransactionStepSetter
    {
        public IList<TransactionStep> Steps
        {
            get {
                return new List<TransactionStep>() {
                    new TransactionStep(){ Name = "Index" , ActionName="Index" , Area = "" , ControllerName = "Home"},
                    new TransactionStep(){ Name = "About" , ActionName="About" , Area = "" , ControllerName = "Home"},
                    new TransactionStep(){ Name = "Contact" , ActionName="Contact" , Area = "" , ControllerName = "Home"}
                };
            }
        }
    }
}
