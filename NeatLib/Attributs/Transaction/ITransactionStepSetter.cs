using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public interface ITransactionStepSetter
    {
        IList<TransactionStep> Steps { get;}
    }
}
