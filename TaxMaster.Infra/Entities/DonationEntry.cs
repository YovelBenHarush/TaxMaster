namespace TaxMaster.Infra;

public class Donations
{
    public List<DonationEntry> DonationsList { get; set; } = [];

    public long TotalDonations => (DonationsList != null && DonationsList.Count > 0) ? DonationsList.Sum(d => long.Parse(d.DonationAmount)) : 0;
}

public class DonationEntry
{
    public DonationEntry()
    {
    }

    public string DonationAmount { get; set; } = string.Empty;
    public string ReciptPath { get; set; } = string.Empty;

    public DonationEntry(DonationEntry other)
    {
        DonationAmount = other.DonationAmount;
        ReciptPath = other.ReciptPath;
    }
}
