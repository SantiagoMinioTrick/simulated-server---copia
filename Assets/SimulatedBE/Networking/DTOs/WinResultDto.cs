namespace SimulatedBE.Networking.DTOs
{
    public struct WinResultDto
    {
        public BreachDto Breach { get; private set; }
        public int RemainingHands { get; private set; }
        public int RemainingDiscards { get; private set; }
        public long Interest { get; private set; }
        public long TotalReward { get; private set; }

        public WinResultDto(BreachDto breach, int remainingHands, int remainingDiscards, long interest, long totalReward)
        {
            Breach = breach;
            RemainingHands = remainingHands;
            RemainingDiscards = remainingDiscards;
            Interest = interest;
            TotalReward = totalReward;
        }
    }
}