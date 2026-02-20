namespace SimulatedBE.Networking.DTOs
{
    public struct CalculatedScoreDto
    {
        public RoundDto Round { get; private set; }
        public bool IsGoalAccomplished { get; private set; }

        public CalculatedScoreDto(RoundDto round, bool isGoalAccomplished)
        {
            Round = round;
            IsGoalAccomplished = isGoalAccomplished;
        }
    }
}
