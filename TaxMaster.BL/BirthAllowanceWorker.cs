using Microsoft.Extensions.Logging;
using TaxMaster.Infra;
using TaxMaster.Infra.Parsers;

namespace TaxMaster.BL
{
    public class BirthAllowanceWorker : BaseWorker
    {
        private readonly BirthPaymentFileParser _birthPaymentFileParser;

        public BirthAllowanceWorker()
        {
            _birthPaymentFileParser = new BirthPaymentFileParser();
        }
        public TaxBirthPaymentFile BirthPayment(string file)
        {
            try
            {
                var taxBirthPaymentFile = _birthPaymentFileParser.ParseBirthPaymentFile(file);
                if(taxBirthPaymentFile.Tax == 0 || taxBirthPaymentFile.Amount == 0)
                {
                    Logger.LogError("Birth payment file is invalid");
                    throw new Exception("Birth payment file is invalid");
                }

                AnnualReportConfiguration.BirthPayment = taxBirthPaymentFile;
                return taxBirthPaymentFile;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
