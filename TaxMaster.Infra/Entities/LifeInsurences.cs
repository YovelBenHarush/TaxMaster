namespace TaxMaster.Infra;

public class LifeInsurences
{
    public List<InsuranceEntry> UserInsurances { get; set; } = [];
    public List<InsuranceEntry> PartnerInsurances { get; set; } = [];
}

public class InsuranceEntry
{
    public string Company { get; set; }
    public string AnnualAmount { get; set; }
    public string PolicyPath { get; set; }
}
