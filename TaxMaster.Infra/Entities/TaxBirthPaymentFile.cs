namespace TaxMaster.Infra
{
    public class TaxBirthPaymentFile
    {
        public long Amount;
        public long Tax;

        public override string ToString()
        {
            return $"BirthPayment: " +
                   $"Amount = {Amount}, " +
                   $"Tax= {Tax}";
        }
    }
}
