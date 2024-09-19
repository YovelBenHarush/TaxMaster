namespace TaxMaster.Infra;

public class LifeInsurences
{
    public List<InsuranceEntry> InsurencesList { get; set; } = [];

    public long TotalInsurencesValue => (InsurencesList != null && InsurencesList.Count > 0) ? InsurencesList.Sum(d => d.AnnualAmount) : 0;

}

public class InsuranceEntry
{
    public InsuranceEntry()
    {
    }

    public string Company { get; set; } = string.Empty;
    public long AnnualAmount { get; set; } = 0;
    public string PolicyPath { get; set; } = string.Empty;

    public InsuranceEntry(InsuranceEntry other)
    {
        Company = other.Company;
        AnnualAmount = other.AnnualAmount;
        PolicyPath = other.PolicyPath;
    }
}
