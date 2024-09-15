namespace TaxMaster.Infra.Interfaces
{
    public interface ISellTransaction
    {
        string ShareIndex { get; set; }

        public double PurchasePriceInUSD { get; set; }

        public double SellPriceInUSD { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SellDate { get; set; }
    }

    public class SellTransaction : ISellTransaction
    {
        public string ShareIndex { get; set; }

        public double PurchasePriceInUSD { get; set; }

        public double SellPriceInUSD { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SellDate { get; set; }
    }
}
