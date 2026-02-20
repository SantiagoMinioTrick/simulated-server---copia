namespace SimulatedBE.Networking.DTOs
{
    public struct EditionDto
    {
        public int ExtraChips { get; private set; }
        public float Multiplier { get; private set; }
        public int ExtraMultiplier { get; private set; }

        public EditionDto(int extraChips,float multiplier, int extraMultiplier)
        {
            ExtraChips = extraChips;
            Multiplier = multiplier;
            ExtraMultiplier = extraMultiplier;
        }
    }
}