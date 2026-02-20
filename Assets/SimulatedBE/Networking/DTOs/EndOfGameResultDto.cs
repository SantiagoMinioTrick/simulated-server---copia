namespace SimulatedBE.Networking.DTOs
{
    public struct EndOfGameResultDto
    {
        public long BestHandScore { get; private set; }
        public PokerHandDto MostPlayerHand { get; private set; }
        public int NumberOfCardsPlayed { get; private set; }
        public int DiscardedCards { get; private set; }
        public int CardsPurchased { get; private set; }
        public int Renewals { get; private set; }
        public int Discoveries { get; private set; }
        public int InitialRound { get; private set; }
        public int RoundNumber { get; private set; }
        public string DefeatedBy { get; set; }

        public EndOfGameResultDto(long bestHandScore, PokerHandDto mostPlayerHand, int numberOfCardsPlayed, int discardedCards, int cardsPurchased, int renewals, int discoveries, int initialRound, int roundNumber, string defeatedBy = "")
        {
            BestHandScore = bestHandScore;
            MostPlayerHand = mostPlayerHand;
            NumberOfCardsPlayed = numberOfCardsPlayed;
            DiscardedCards = discardedCards;
            CardsPurchased = cardsPurchased;
            Renewals = renewals;
            Discoveries = discoveries;
            InitialRound = initialRound;
            RoundNumber = roundNumber;
            DefeatedBy = defeatedBy;
        }
    }
}