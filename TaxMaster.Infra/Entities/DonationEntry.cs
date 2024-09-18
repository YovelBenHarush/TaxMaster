namespace TaxMaster.Infra;

public class Donations
{
    public List<DonationEntry> DonationsList { get; set; } = [];
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
