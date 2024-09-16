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
