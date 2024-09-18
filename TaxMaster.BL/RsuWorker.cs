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
    public class RsuWorker : BaseWorker
    {
        public RsuWorker()
        {

        }

        public async Task<RsuEsopObject> RsuEsopAsync(string esopTransactionsReportFilePath, string tax867FilePath)
        {
            var rsuEsopParser = new RsuEsopParser();
            if (!string.IsNullOrEmpty(tax867FilePath))
            {
                var tax867FileName = ReportSettings.Configuration.RegisteredPartner.ID + "_" + ConstNamesConfiguration.Report867 + ".pdf";
                SaveToOutputDir(tax867FilePath, tax867FileName);
                var output = rsuEsopParser.ParseDividend(tax867FilePath);
                ReportSettings.Configuration.RsuEsopObject.DividendInNis = output.dividendInNis;
                ReportSettings.Configuration.RsuEsopObject.DividendTaxInNis = output.dividendTaxInNis;
            }

            if (!string.IsNullOrEmpty(esopTransactionsReportFilePath))
            {
                var esopTransactionsFileName = ReportSettings.Configuration.RegisteredPartner.ID + "_" + ConstNamesConfiguration.EsopTransactionsReport + ".pdf";
                SaveToOutputDir(esopTransactionsReportFilePath, esopTransactionsFileName);
                ReportSettings.Configuration.RsuEsopObject.Transactions = rsuEsopParser.ParseStockSalesTranscations(esopTransactionsReportFilePath);
            }

            return ReportSettings.Configuration.RsuEsopObject;
        }
    }
}

