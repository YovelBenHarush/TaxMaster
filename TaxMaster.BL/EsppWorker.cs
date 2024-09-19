using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;
using TaxMaster.Infra.Configuration;
using TaxMaster.Infra.Entities;
using TaxMaster.Infra.Parsers;

namespace TaxMaster.BL
{
    public class EsppWorker : BaseWorker
    {
        public EsppWorker()
        {

        }

        public async Task<EsppObject> EsppFidelityAsync(bool isRegisteredPartner, string tax1042sFilePath, string customTransactionSummaryFilePath)
        {
            var esppFidelityClient = new ESPPFidelityParser();
            var user= isRegisteredPartner ? ReportSettings.Configuration.RegisteredPartner : ReportSettings.Configuration.Partner;
            if (!string.IsNullOrEmpty(tax1042sFilePath))
            {
                var tax1042sFileName = user.ID + "_" + ConstNamesConfiguration.Report1042s + ".pdf";
                SaveToOutputDir(tax1042sFilePath, tax1042sFileName);
            }

            if (!string.IsNullOrEmpty(customTransactionSummaryFilePath))
            {
                var sellTransactions = esppFidelityClient.ParseStockSalesTranscations(customTransactionSummaryFilePath);
                var esppDivident = esppFidelityClient.ParseDividend(customTransactionSummaryFilePath);

                var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();
                var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransactions);
                user.EsppObject.TransactionWithTaxMetadata = sellTransactionsWithTaxMetadata;
                var parser = new Form1325Parser();
                var outputPaths = parser.Generate1325Forms(sellTransactionsWithTaxMetadata, user.ID, GetOutputDir());

                user.EsppObject.FirstHalfOfYearStockSaleReport = outputPaths.FirstHalfFormPath;
                user.EsppObject.SecondHalfOfYearStockSaleReport = outputPaths.SecondHalfFormPath;
                user.EsppObject.DividendInUsd = esppDivident;
            }

            return user.EsppObject;
        }
    }
}

