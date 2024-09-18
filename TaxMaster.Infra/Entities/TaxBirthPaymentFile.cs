namespace TaxMaster.Infra
{
    public class TaxBirthPaymentFile
    {
        public double Amount;
        public double Tax;

        public string FilePath;
        public string UserId;

        public override string ToString()
        {
            return $"BirthPayment: " +
                   $"Amount = {Amount}, " +
                   $"Tax= {Tax}";
        }
    }
}
