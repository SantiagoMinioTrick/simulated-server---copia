namespace SimulatedBE.Networking.DTOs
{
    public struct SelectBreachResultDto
    {
        public RoundDto CurrentRound { get; private set;}
        public BreachDto Breach { get; private set;}
        public long PlayerTreasure { get; private set; }
        public SelectBreachResultDto(RoundDto currentRound, BreachDto breach, long playerTreasure)
        {
            CurrentRound = currentRound;
            Breach = breach;
            PlayerTreasure = playerTreasure;
        }
    }
}