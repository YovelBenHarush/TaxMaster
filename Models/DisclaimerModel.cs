namespace TaxMaster
{
    public class DisclaimerModel : BaseModel
    {
        public string DisclaimerText { get; set; }
        public string DisclaimerAprovalText { get; set; }
        public bool DisclaimerAproval { get; set; }
        public bool LogAproval { get; set; }
    }
}
