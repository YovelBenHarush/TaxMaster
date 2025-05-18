using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using FileHelpers;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.Infra.Parsers;

public class IbkrEsppCsvParser
{
    public List<ISellTransaction> ParseStockSalesTranscations(string filePath)
    {
        var sellTransactions = new List<ISellTransaction>();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        };

        int startIndx = 0;
        using (var reader = new StreamReader(filePath))
        {
            while (reader.Peek() > 0)
            {
                var line = reader.ReadLine();
                if (line != null && line.StartsWith("Trades"))
                {
                    break;
                }
                startIndx++;
            }
        }

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            // Skip lines until the row starts with "Trades"
            int headersRow = 0;
            while (reader.Peek() > 0 && headersRow++ < startIndx)
            {
                var line = reader.ReadLine();
            }

            // Read the header row
            csv.Read();
            csv.ReadHeader();

            var records = csv.GetRecords<TradeRecord>().ToList();

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];
                if (record.DataDiscriminator == "Trade")
                {
                    while (i + 1 < records.Count && records[i + 1].DataDiscriminator == "ClosedLot")
                    {
                        var lotRecord = records[++i];
                        var sellTransaction = new SellTransaction
                        {
                            ShareIndex = record.Symbol,
                            PurchasePriceInUSD = lotRecord.Basis,
                            SellPriceInUSD = lotRecord.Basis + lotRecord.RealizedPL,
                            PurchaseDate = lotRecord.DateTime, // Assuming DateTime is the purchase date
                            SellDate = record.DateTime // Assuming DateTime is the sell date
                        };

                        sellTransactions.Add(sellTransaction);
                    }
                }
            }
        }

        return sellTransactions;
    }

    public double ParseDividend(string filePath)
    {
        double dividend = 0;
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        };
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            // Skip lines until the row starts with "Trades"
            int headersRow = 0;
            while (reader.Peek() > 0 && headersRow++ < 2)
            {
                var line = reader.ReadLine();
            }
            // Read the header row
            csv.Read();
            csv.ReadHeader();
            var records = csv.GetRecords<TradeRecord>().ToList();
            foreach (var record in records)
            {
                if (record.DataDiscriminator == "Dividend")
                {
                    dividend += record.Proceeds;
                }
            }
        }
        return dividend;
    }
}

public class TradeRecord
{
    [Name("DataDiscriminator")]
    public string DataDiscriminator { get; set; }

    [Name("Asset Category")]
    public string AssetCategory { get; set; }

    [Name("Currency")]
    public string Currency { get; set; }

    [Name("Symbol")]
    public string Symbol { get; set; }

    [Name("Date/Time")]
    [Format(["yyyy-MM-dd, HH:mm:ss", "MM/dd/yyyy", "yyyy-MM-dd", "M/dd/yyyy"])]
    [Default("1900-01-01")]
    public DateTime DateTime { get; set; }

    [Name("Exchange")]
    public string Exchange { get; set; }

    [Name("Quantity")]
    [Default(0)]
    public double Quantity { get; set; }

    [Name("T. Price")]
    [Default(0)]
    public double TradePrice { get; set; }

    [Name("C. Price")]
    [Default(0)]
    public double ClosePrice { get; set; }

    [Name("Proceeds")]
    [Default(0)]
    public double Proceeds { get; set; }

    [Name("Comm/Fee")]
    [Default(0)]
    public double CommFee { get; set; }

    [Name("Basis")]
    [Default(0)]
    public double Basis { get; set; }

    [Name("Realized P/L")]
    [Default(0)]
    public double RealizedPL { get; set; }

    [Name("MTM P/L")]
    [Default(0)]
    public double MTMPL { get; set; }

    [Name("Code")]
    public string Code { get; set; }
}

public class TradeRecordMap : ClassMap<TradeRecord>
{
    public TradeRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(x => x.DateTime).TypeConverterOption.Format("yyyy-MM-dd, HH:mm:ss");
        Map(x => x.DateTime).TypeConverterOption.Format("MM-dd-yyyy");
        Map(x => x.DateTime).TypeConverterOption.Format("MM/dd/yyyy");
        Map(x => x.DateTime).TypeConverterOption.Format("M/dd/yyyy");
    }
}

[DelimitedRecord(",")]
public class TradeRecord1
{
    public string DataDiscriminator { get; set; }
    public string AssetCategory { get; set; }
    public string Currency { get; set; }
    public string Symbol { get; set; }
    [FieldConverter(ConverterKind.Date, "yyyy-MM-dd, HH:mm:ss")]
    public DateTime DateTime { get; set; }
    public string Exchange { get; set; }
    public int Quantity { get; set; }
    public double TradePrice { get; set; }
    public double ClosePrice { get; set; }
    public double Proceeds { get; set; }
    public double CommFee { get; set; }
    public double Basis { get; set; }
    public double RealizedPL { get; set; }
    public double MTMPL { get; set; }
    public string Code { get; set; }
}