namespace TaxMaster.BL.CapitalGainTaxCaclulator.Interfaces
{
    public interface ISellTransaction
    {
        string ShareIndex { get; set; }

        public double PurchasePrice { get; set; }

        public double SellPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SellDate { get; set; }
    }

    public class SellTransaction : ISellTransaction
    {
        public string ShareIndex { get; set; }

        public double PurchasePrice { get; set; }

        public double SellPrice { get; set; }

        public DateTime PurchaseDate { get; set; }

        public DateTime SellDate { get; set; }
    }
}
