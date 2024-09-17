using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.Infra.Entities
{
    public class EsppObject
    {
        public string FirstHalfOfYearStockSaleReport { get; set; }
        public string SecondHalfOfYearStockSaleReport { get; set; }
        public double Dividend { get; set; }
        public IEnumerable<ISellTransactionWithTaxMetadata> TransactionWithTaxMetadata { get; set; }
    }
}
