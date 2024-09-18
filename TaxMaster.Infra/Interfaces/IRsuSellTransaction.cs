using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.Infra.Interfaces
{
    public interface IRsuSellTransaction
    {
        public string ShareIndex { get; set; }

        public double PurchasePriceInNis { get; set; }

        public double OrdinaryIncomeInNis { get; set; }

        public DateTime GrantDate { get; set; }

        public DateTime SellDate { get; set; }
    }

    public class RsuSellTransaction : IRsuSellTransaction
    {
        public string ShareIndex { get; set; }

        public double PurchasePriceInNis { get; set; }

        public double OrdinaryIncomeInNis { get; set; }

        public DateTime GrantDate { get; set; }

        public DateTime SellDate { get; set; }
    }
}
