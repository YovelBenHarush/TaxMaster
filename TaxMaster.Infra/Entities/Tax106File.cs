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
        public long _193_093 { get; set; }


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

    public class Tax106FileWrapper
    {
        public List<Tax106File>? taxFiles { get; set; } = new();

        public Tax106File GetMerged106()
        {
            var merged = new Tax106File();

            if (taxFiles != null)
            {
                foreach (var file in taxFiles)
                {
                    merged._158_172 += file._158_172;
                    merged._042 += file._042;
                    merged._244_245 += file._244_245;
                    merged._218_219 += file._218_219;
                    merged._086_045 += file._086_045;
                    merged._248_249 += file._248_249;
                    merged._037_237 += file._037_237;
                    merged._193_093 += file._193_093;
                }
            }

            return merged;
        }
    }
}
