using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;
using TaxMaster.Infra.Parsers;

namespace TaxMaster.BL
{

    public class Tax106FileWorker : BaseWorker
    {
        public Tax106FileWorker()
        {

        }

        public async void Submit(string tax106FilePath, Tax106File tax106File, bool isFiller)
        {
            if (isFiller)
            {
                var tax106OutputNameFiller = ReportSettings.Configuration.RegisteredPartner.ID + "_" + "106";
                SaveToOutputDir(tax106FilePath, tax106OutputNameFiller);
                ReportSettings.Configuration.Tax106Files.User106 = tax106File;
            }
            else
            {
                var tax106OutputNamePartner = ReportSettings.Configuration.Partner.ID + "_" + "106";
                SaveToOutputDir(tax106FilePath, tax106OutputNamePartner);
                ReportSettings.Configuration.Tax106Files.Partner106 = tax106File;
            }
        }
    }
}

