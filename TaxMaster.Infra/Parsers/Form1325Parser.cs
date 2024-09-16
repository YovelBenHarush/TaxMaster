﻿using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf;
using TaxMaster.Infra.Interfaces;
using PdfSharpCore.Pdf.Content.Objects;
using PdfSharpCore.Pdf.Content;
using Spire.Xls;

namespace TaxMaster.Infra.Parsers
{
    public class Form1325Parser
    {
        private const int startRow = 5;
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
        private const string TotalTaxableProfitCol = "K17";
        private const string TotalSellPriceCol = "K18";
        private const string NameCol = "E2";
        private const string IDCol = "G2";

        private const string Form1325Path = "Assets\\1325Form.xlsx";
        private const string Pdf1325PathTemaplate = "{0}_Generated1325Form.pdf";

        public string Generate1325Form(IEnumerable<ISellTransactionWithTaxMetadata> transactions, string outputDir)
        {
            return Populate1325AndSaveToPdf(Path.Combine(Directory.GetCurrentDirectory(), Form1325Path), outputDir, transactions);
        }

        public string Populate1325AndSaveToPdf(string xlsxFilePath, string outputDir, IEnumerable<ISellTransactionWithTaxMetadata> transactions)
        {
            var workbook = LoadXlWorkbook(xlsxFilePath);
            var pdfFilePath = Path.Combine(outputDir, string.Format(Pdf1325PathTemaplate,AnnualReportConfiguration.RegisteredPartner.ID));
            PopulateXlWorkbook(workbook, pdfFilePath, transactions);
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

        public void PopulateXlWorkbook(Workbook workbook, string pdfFilePath, IEnumerable<ISellTransactionWithTaxMetadata> transactions)
        {
            // Get the first worksheet
            Worksheet sheet = workbook.Worksheets[0];

            for (int i = 0; i < transactions.Count(); i++)
            {
                var transaction = transactions.ElementAt(i);

                // Edit cell A1
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

            sheet.Range[Col(TotalTaxableProfitCol)].NumberValue = transactions.Sum(t => t.TaxableProfitInILS);
            sheet.Range[Col(TotalSellPriceCol)].NumberValue = transactions.Sum(t => t.SellPriceInILS);

            sheet.Range[Col(NameCol)].Text = $"{AnnualReportConfiguration.RegisteredPartner.FirstName} {AnnualReportConfiguration.RegisteredPartner.LastName}";
            sheet.Range[Col(IDCol)].Text = $"{AnnualReportConfiguration.RegisteredPartner.ID}";
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
