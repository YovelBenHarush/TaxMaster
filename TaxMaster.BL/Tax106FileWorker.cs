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
                var tax106OutputNameFiller = ReportSettings.Configuration.RegisteredPartner.ID + "_" + "106";
                SaveToOutputDir(tax106FilePaths, tax106OutputNameFiller);

                ReportSettings.Configuration.RegisteredPartner.Tax106FilesWrapper = new() { taxFiles = tax106Files };
            }
            else
            {
                var tax106OutputNamePartner = ReportSettings.Configuration.Partner.ID + "_" + "106";
                SaveToOutputDir(tax106FilePaths, tax106OutputNamePartner);

                ReportSettings.Configuration.Partner.Tax106FilesWrapper = new() { taxFiles = tax106Files };
            }
        }

        private void SaveToOutputDir(List<string> filePaths, string outputName)
        {
            for (int i = 0; i < filePaths.Count; i++)
            {
                var filePath = filePaths[i];
                SaveToOutputDir(filePath, outputName + "_" + (i + 1));
            }
        }
    }
}

