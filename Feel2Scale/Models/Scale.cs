namespace Feel2Scale.Models
{
    public class Scale
    {
        private static int SIZE_OF_SCALE = 6;

        public string? ScaleName { get; set; }
        public string? ScaleDescription { get; set; }
        
        public int[] Numbers { get; set; } = new int[SIZE_OF_SCALE];

        public string[] Chords { get; set; } = new string[SIZE_OF_SCALE];
    }
}
