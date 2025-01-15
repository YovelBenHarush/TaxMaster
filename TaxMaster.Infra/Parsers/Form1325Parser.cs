using TaxMaster.Infra.Interfaces;
using Spire.Xls;
using TaxMaster.Infra.Entities;

namespace TaxMaster.Infra.Parsers
{
    public class Form1325Parser
    {
        private const int startRow = 7;
        private const string ShareIndexCol = "B";
        private const string SellPriceInUSDCol = "D";
        private const string PurchaseDateCol = "E";
        private const string PurchasePriceInILSCol = "F";
        private const string ExchangeRateCol = "G";
        private const string AdjustedPurchasePriceinILSCol = "H";
        private const string SellDateCol = "I";
        private const string SellPriceInILSCol = "J";
        private const string TaxableProfitInILSCol = "K";
        private const string LossinILSCol = "L";
        private const string TotalTaxableProfitCol = "K29";
        private const string TotalLossCol = "L29";
        private const string TotalSellPriceCol = "K30";
        private const string NameCol = "E4";
        private const string IDCol = "G4";

        private const string Form1325Path = "Assets\\1325Form.xlsx";
        private const string Pdf1325PathTemaplate = "{0}_1325_{1}.pdf";

        public (string FirstHalfFormPath, string SecondHalfFormPath) Generate1325Forms(IEnumerable<ISellTransactionWithTaxMetadata> transactions, User user, string outputDir)
        {
            var firstHalfTransactions = transactions.Where(trx => trx.SellDate.Month <= 6);
            var secondHalfTransactions = transactions.Where(trx => trx.SellDate.Month > 6);

            return (Populate1325Form(Path.Combine(outputDir, string.Format(Pdf1325PathTemaplate, user.ID, 1)), firstHalfTransactions, user), Populate1325Form(Path.Combine(outputDir, string.Format(Pdf1325PathTemaplate, user.ID, 2)), secondHalfTransactions, user));
        }

        public string Populate1325Form(string pdfFilePath, IEnumerable<ISellTransactionWithTaxMetadata> transactions, User user)
        {
            if (!transactions.Any())
                return String.Empty;

            var workbook = LoadXlWorkbook(Path.Combine(Directory.GetCurrentDirectory(), Form1325Path));
            PopulateXlWorkbook(workbook, pdfFilePath, transactions, user);
            SaveXlWorkbookToPdf(workbook, pdfFilePath);
            return pdfFilePath;
        }
        public Workbook LoadXlWorkbook(string xlsxFilePath)
        {
            //Create a Workbook instance
            Workbook workbook = new Workbook();

            //Load a sample Excel file
            workbook.LoadFromFile(xlsxFilePath);

            //Set worksheets to fit to page when converting
            workbook.ConverterSetting.SheetFitToPage = true;

            // Get the first worksheet
            return workbook;
        }

        public void PopulateXlWorkbook(Workbook workbook, string pdfFilePath, IEnumerable<ISellTransactionWithTaxMetadata> transactions, User user)
        {
            // Get the first worksheet
            Worksheet sheet = workbook.Worksheets[0];

            for (int i = 0; i < transactions.Count(); i++)
            {
                var transaction = transactions.ElementAt(i);

                sheet.Range[Col(ShareIndexCol, i)].Text = transaction.ShareIndex;
                sheet.Range[Col(SellPriceInUSDCol, i)].NumberValue = transaction.SellPriceInUSD;
                sheet.Range[Col(PurchaseDateCol, i)].DateTimeValue = transaction.PurchaseDate;
                sheet.Range[Col(PurchasePriceInILSCol, i)].NumberValue = transaction.PurchasePriceInILS;
                sheet.Range[Col(ExchangeRateCol, i)].NumberValue = transaction.ExchangeRate;
                sheet.Range[Col(AdjustedPurchasePriceinILSCol, i)].NumberValue = transaction.AdjustedPurchasePriceInILS;
                sheet.Range[Col(SellDateCol, i)].DateTimeValue = transaction.SellDate;
                sheet.Range[Col(SellPriceInILSCol, i)].NumberValue = transaction.SellPriceInILS;

                if (transaction.TaxableProfitInILS > 0)
                {
                    sheet.Range[Col(TaxableProfitInILSCol, i)].NumberValue = transaction.TaxableProfitInILS;
                }
                else
                {
                    sheet.Range[Col(LossinILSCol, i)].NumberValue = transaction.TaxableProfitInILS;
                }
            }

            sheet.Range[Col(TotalTaxableProfitCol)].NumberValue = transactions.Where(t => t.TaxableProfitInILS > 0).Sum(t => t.TaxableProfitInILS);
            sheet.Range[Col(TotalLossCol)].NumberValue = transactions.Where(t => t.TaxableProfitInILS < 0).Sum(t => t.TaxableProfitInILS);
            sheet.Range[Col(TotalSellPriceCol)].NumberValue = transactions.Sum(t => t.SellPriceInILS);

            sheet.Range[Col(NameCol)].Text = $"{user.FirstName} {user.LastName}";
            sheet.Range[Col(IDCol)].Text = $"{user.ID}";
        }

        public void SaveXlWorkbookToPdf(Workbook workbook, string pdfFilePath)
        {
            workbook.SaveToFile(pdfFilePath, FileFormat.PDF);
        }

        private string Col(string colName, int transactionIndx = -1)
        {
            return transactionIndx < 0 ? colName: $"{colName}{startRow + transactionIndx}";
        }
    }
}
