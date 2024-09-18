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
            var tax867FileName = ReportSettings.Configuration.RegisteredPartner.ID + "_" + ConstNamesConfiguration.Report867 + ".pdf";
            SaveToOutputDir(tax867FilePath, tax867FileName);
            var esopTransactionsFileName = ReportSettings.Configuration.RegisteredPartner.ID + "_" + ConstNamesConfiguration.EsopTransactionsReport + ".pdf";
            SaveToOutputDir(esopTransactionsReportFilePath, esopTransactionsFileName);
            var rsuEsopParser = new RsuEsopParser();
            ReportSettings.Configuration.RsuEsopObject.Transactions = rsuEsopParser.ParseStockSalesTranscations(esopTransactionsReportFilePath);
            var output = rsuEsopParser.ParseDividend(tax867FilePath);
            ReportSettings.Configuration.RsuEsopObject.DividendInNis = output.dividendInNis;
            ReportSettings.Configuration.RsuEsopObject.DividendTaxInNis = output.dividendTaxInNis;

            return ReportSettings.Configuration.RsuEsopObject;
        }
    }
}

