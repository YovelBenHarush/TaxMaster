using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra.Entities;

namespace TaxMaster.BL
{

    public class EsppWorker : BaseWorker
    {
        public EsppWorker()
        {

        }

        public EsppObjetc EsppFidelity(string tax1042sFilePath, string customTransactionSummaryFilePath)
        {
            return new EsppObjetc
            {
                FirstHalfOfYearStockSaleReport = "FirstHalfOfYearStockSaleReport",
                SecondHalfOfYearStockSaleReport = "SecondHalfOfYearStockSaleReport",
                Dividend = 1000
            };
        }
    }
}

