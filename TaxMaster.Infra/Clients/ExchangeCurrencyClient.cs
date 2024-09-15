using System.Globalization;
using System.Net;
using System.Xml.Linq;

namespace TaxMaster.TaxMaster.Infra;

public class ExchangeCurrencyClient
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<double> GetExchangeRateAsync(DateTime date)
    {
        int cntr = 0;
        HttpResponseMessage response;
        do
        {
            date = date.AddDays(-cntr);
            string formattedDate = date.ToString("yyyy-MM-dd");
            string url = $"https://edge.boi.org.il/FusionEdgeServer/sdmx/v2/data/dataflow/BOI.STATISTICS/EXR/1.0/RER_USD_ILS?c%5BDATA_TYPE%5D=OF00&startperiod={formattedDate}&endperiod={formattedDate}&format=csv";

            response = await client.GetAsync(url);
        }
        while (cntr++ < 10 && response.StatusCode == HttpStatusCode.NotFound);

        response.EnsureSuccessStatusCode();

        string csvContent = await response.Content.ReadAsStringAsync();
        using (var reader = new StringReader(csvContent))
        {
            string? headerLine = reader.ReadLine();
            string? dataLine = reader.ReadLine();

            if (headerLine == null || dataLine == null)
            {
                throw new Exception("Invalid CSV format.");
            }

            string[] headers = headerLine.Split(',');
            string[] data = dataLine.Split(',');

            int obsValueIndex = Array.IndexOf(headers, "OBS_VALUE");
            if (obsValueIndex == -1)
            {
                throw new Exception("OBS_VALUE column not found.");
            }

            if (double.TryParse(data[obsValueIndex], NumberStyles.Any, CultureInfo.InvariantCulture, out double exchangeRate))
            {
                return exchangeRate;
            }

            throw new Exception("Invalid OBS_VALUE format.");
        }
    }
}
