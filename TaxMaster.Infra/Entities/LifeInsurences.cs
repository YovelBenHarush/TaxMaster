namespace TaxMaster.Infra;

public class LifeInsurences
{
    public List<InsuranceEntry> UserInsurances { get; set; } = [];
    public List<InsuranceEntry> PartnerInsurances { get; set; } = [];
}

public class InsuranceEntry
{
    public InsuranceEntry()
    {
    }

    public string Company { get; set; } = string.Empty;
    public string AnnualAmount { get; set; } = string.Empty;
    public string PolicyPath { get; set; } = string.Empty;

    public InsuranceEntry(InsuranceEntry other)
    {
        Company = other.Company;
        AnnualAmount = other.AnnualAmount;
        PolicyPath = other.PolicyPath;
    }
}
