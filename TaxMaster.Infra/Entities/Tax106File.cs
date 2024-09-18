namespace TaxMaster.Infra
{
    public class Tax106File
    {
        public long _158_172;
        public long _042;
        public long _244_245;
        public long _218_219;
        public long _086_045;
        public long _248_249;
        public long _037_237;
        public long _193_093;

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
        public Tax106File? User106 { get; set; } = null;
        public Tax106File? Partner106 { get; set; } = null;
    }
}
