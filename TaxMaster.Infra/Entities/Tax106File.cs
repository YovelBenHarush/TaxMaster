namespace TaxMaster.Infra
{
    public class Tax106File
    {
        public long _158_172 { get; set; }
        public long _042 { get; set; }
        public long _244_245 { get; set; }
        public long _218_219 { get; set; }
        public long _086_045 { get; set; }
        public long _248_249 { get; set; }
        public long _037_237 { get; set; }

        // Override the ToString method to return a string representation of the object
        public override string ToString()
        {
            return $"Tax106: " +
                   $"_158_172 = {_158_172}, " +
                   $"_042 = {_042}, " +
                   $"_244_245 = {_244_245}, " +
                   $"_218_219 = {_218_219}, " +
                   $"_086_045 = {_086_045}, " +
                   $"_248_249 = {_248_249}, " +
                   $"_037_237 = {_037_237}, " +
                  $"_193_093 = {_193_093}, ";
        }
    }
    public class Tax106Files
    {
        public List<Tax106File>? User106 { get; set; } = new();
        public List<Tax106File>? Partner106 { get; set; } = new();
    }
}
