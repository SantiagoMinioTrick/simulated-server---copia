namespace SimulatedBE.Networking.DTOs
{
    public struct ScoreDto
    {
        public long ScoreValue { get; private set;}
        public long ChipsValue { get; private set; }
        public long MultiplierValue { get; private set; }
        
        public ScoreDto(long scoreValue, long chipsValue ,long multiplierValue)
        {
            ScoreValue = scoreValue;
            ChipsValue = chipsValue;
            MultiplierValue = multiplierValue;
        }
    }
}