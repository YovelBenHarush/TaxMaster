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

        public void Submit(List<string> tax106FilePaths, List<Tax106File> tax106Files, bool isFiller)
        {
            if (isFiller)
            {
                var tax106OutputNameFiller = ReportSettings.Configuration.RegisteredPartner.ID;
                SaveToOutputDir(tax106FilePaths, tax106OutputNameFiller);

                ReportSettings.Configuration.RegisteredPartner.Tax106FilesWrapper = new() { taxFiles = tax106Files };
            }
            else
            {
                var tax106OutputNamePartner = ReportSettings.Configuration.Partner.ID;
                SaveToOutputDir(tax106FilePaths, tax106OutputNamePartner);

                ReportSettings.Configuration.Partner.Tax106FilesWrapper = new() { taxFiles = tax106Files };
            }
        }

        private void SaveToOutputDir(List<string> filePaths, string outputName)
        {
            var random = new Random();
            for (int i = 0; i < filePaths.Count; i++)
            {
                var filePath = filePaths[i];
                var randomHex = random.Next(0x100000, 0x1000000).ToString("X5");
                SaveToOutputDir(filePath, outputName + "_106_" + randomHex);
            }
        }
    }
}

