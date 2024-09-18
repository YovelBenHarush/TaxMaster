using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.Infra.Entities
{
    public class RsuEsopObject
    {
        public double DividendInNis { get; set; }
        public double DividendTaxInNis { get; set; }

        public IEnumerable<IRsuSellTransaction> Transactions{ get; set; }
    }
}
