﻿using TaxMaster.BL.CapitalGainTaxCaclulator;
using TaxMaster.Infra;
using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra.Parsers;

public static class Program
{
    public static async Task Main(string[] args)
    {
        //var esppFidelityClient = new ESPPFidelityParser();
        //var CustomTransactionSummaryFilePath = "CustomTransactionsummary2021.pdf";
        //var transactions = esppFidelityClient.ParseStockSalesTranscations(CustomTransactionSummaryFilePath);
        //esppFidelityClient.ParseDividend(CustomTransactionSummaryFilePath);

        var sellTransaction = new SellTransaction
        {
            ShareIndex = "MSFT",
            PurchasePriceInUSD = 1000,
            SellPriceInUSD = 1500,
            PurchaseDate = DateTime.Now - TimeSpan.FromDays(50),
            SellDate = DateTime.Now
        };

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();

        ISellTransactionWithTaxMetadata sellTransactionWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransaction);

        Console.WriteLine(
            $"Share index: {sellTransactionWithTaxMetadata.ShareIndex}, " +
            $"Purchase price: {sellTransactionWithTaxMetadata.PurchasePriceInUSD}, " +
            $"Sell price: {sellTransactionWithTaxMetadata.SellPriceInUSD}, " +
            $"Purchase date: {sellTransactionWithTaxMetadata.PurchaseDate}, " +
            $"Sell date: {sellTransactionWithTaxMetadata.SellDate}, " +
            $"Tax rate: {sellTransactionWithTaxMetadata.TaxRate}, " +
            $"Exchange rate: {sellTransactionWithTaxMetadata.ExchangeRate}, " +
            $"Tax amount: {sellTransactionWithTaxMetadata.TaxAmountToPay}");

        var parser = new Form1325Parser();
        parser.Generate1325Form([sellTransactionWithTaxMetadata, sellTransactionWithTaxMetadata, sellTransactionWithTaxMetadata]);
    }
}