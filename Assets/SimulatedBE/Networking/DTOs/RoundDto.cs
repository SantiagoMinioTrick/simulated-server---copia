namespace SimulatedBE.Networking.DTOs
{
    public struct RoundDto
    {
        public int AmountOfHands { get; private set; }
        public int AmountOfDiscards { get; private set; }
        public int CurrentRound { get; private set; }
        public int InitialBet { get; private set; }
        public ScoreDto Score { get; private set; }

        public RoundDto(int amountOfHands, int amountOfDiscards, int currentRound, int initialBet, ScoreDto score)
        {
            AmountOfHands = amountOfHands;
            AmountOfDiscards = amountOfDiscards;
            CurrentRound = currentRound;
            InitialBet = initialBet;
            Score = score;
        }

        public RoundDto SetScore(ScoreDto score)
        {
            this.Score = score;
            return this;
        }
    }
}