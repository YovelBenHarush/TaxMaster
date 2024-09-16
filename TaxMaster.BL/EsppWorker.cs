using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;
using TaxMaster.Infra.Parsers;

namespace TaxMaster.BL
{

    public class EsppWorker : BaseWorker
    {
        public EsppWorker()
        {

        }

        public async Task<EsppObject> EsppFidelityAsync(string tax1042sFilePath, string customTransactionSummaryFilePath)
        {
            SaveToOutputDir(tax1042sFilePath);
            var esppFidelityClient = new ESPPFidelityParser();
            var sellTransactions = esppFidelityClient.ParseStockSalesTranscations(customTransactionSummaryFilePath);
            var esppDivident = esppFidelityClient.ParseDividend(customTransactionSummaryFilePath);

            var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();
            var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransactions);
            var parser = new Form1325Parser();
            var outputPath = parser.Generate1325Forms(sellTransactionsWithTaxMetadata , GetOutputDir());

            return new EsppObject
            {
                FirstHalfOfYearStockSaleReport = outputPath.FirstHalfFormPath,
                SecondHalfOfYearStockSaleReport = outputPath.SecondHlfFormPath,
                Dividend = esppDivident
            };
        }
    }
}

