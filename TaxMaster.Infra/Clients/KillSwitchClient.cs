using System.Net;
using System.Text.Json;

namespace TaxMaster.Infra;

public class KillSwitchClient
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<KillSwitchResponse> GetkillSwitchInfo(string version)
    {
        int cntr = 0;
        HttpResponseMessage response;

        string url = $"https://prod-16.centralus.logic.azure.com/workflows/a7138d58064b499a873e128b5e5544da/triggers/manual/paths/invoke/taxmater?api-version=2016-10-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=mXkivBBIzx-nJsjZXXB0EXt-aawH9lvp8mThZzn10WE";
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(JsonSerializer.Serialize(new { version }), System.Text.Encoding.UTF8, "application/json");
        response = await client.SendAsync(request, CancellationToken.None);

        if (response == null)
        {
            return KillSwitchResponse.AnErrorOccurredInsance("נכשלנו בביצוע קריאת API המערכת לא יכולה לעלות, נסה שוב בהמשך");
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var content = await response.Content.ReadAsStringAsync();
            var killSwitchResponse = JsonSerializer.Deserialize<KillSwitchResponse>(content);
            return killSwitchResponse ?? KillSwitchResponse.AnErrorOccurredInsance("קריאת הAPI החזירה ערך לא צפוי, נסה שוב מאוחר יותר");
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await response.Content.ReadAsStringAsync();
            return KillSwitchResponse.AnErrorOccurredInsance("נכשלנו בביצוע קריאת API המערכת לא יכולה לעלות, נסה שוב בהמשך");
        }

        return KillSwitchResponse.AnErrorOccurredInsance("נכשלנו בביצוע קריאת API המערכת לא יכולה לעלות, נסה שוב בהמשך");
    }
}

public class KillSwitchResponse
{
    public string Text { get; set; }
    public bool canRun { get; set; }

    public static KillSwitchResponse AnErrorOccurredInsance(string text)
    {
        return new KillSwitchResponse
        {
            Text = text,
            canRun = false
        };
    }
}
