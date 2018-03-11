using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Attributs.Transaction
{
    public class TransactionStep : IEquatable<TransactionStep>
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public bool Equals(TransactionStep other)
        {
            if (other == null)
                return false;

            return String.Equals(this.Name, other.Name,
                                StringComparison.OrdinalIgnoreCase);

        }
    }
}
