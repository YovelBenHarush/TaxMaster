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
        public double DividendInUsd { get; set; }
        public string Tax1042sFilePath { get; set; }
        public IEnumerable<ISellTransactionWithTaxMetadata> TransactionsWithTaxMetadata { get; set; }

        public double TotalTaxableProfitInILS => TransactionsWithTaxMetadata?.Sum(t => t.TaxableProfitInILS) ?? 0;

    }
}
