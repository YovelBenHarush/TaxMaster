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

        public async Task<RsuEsopObject> RsuEsopAsync(bool isRegisteredPartner, string esopTransactionsReportFilePath, string tax867FilePath)
        {
            var rsuEsopParser = new RsuEsopParser();
            var user = isRegisteredPartner ? ReportSettings.Configuration.RegisteredPartner : ReportSettings.Configuration.Partner;
            if (!string.IsNullOrEmpty(tax867FilePath))
            {
                var tax867FileName = user.ID + "_" + ConstNamesConfiguration.Report867 + ".pdf";
                SaveToOutputDir(tax867FilePath, tax867FileName);
                var output = rsuEsopParser.ParseDividend(tax867FilePath);
                user.RsuEsopObject.DividendInNis = output.dividendInNis;
                user.RsuEsopObject.DividendTaxInNis = output.dividendTaxInNis;
            }

            if (!string.IsNullOrEmpty(esopTransactionsReportFilePath))
            {
                var esopTransactionsFileName = user.ID + "_" + ConstNamesConfiguration.EsopTransactionsReport + ".pdf";
                SaveToOutputDir(esopTransactionsReportFilePath, esopTransactionsFileName);
                user.RsuEsopObject.Transactions = rsuEsopParser.ParseStockSalesTranscations(esopTransactionsReportFilePath);
            }

            return user.RsuEsopObject;
        }
    }
}

