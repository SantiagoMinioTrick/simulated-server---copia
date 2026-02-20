using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    public struct PlayHandResultDto
    {
        public PokerHandDto PokerHand { get; private set; }
        public List<CardDto> ScoredCards { get; private set; }
        public List<CardDto> CardsPlayed { get; private set; }
        public List<CardDto> NewCards { get; private set; }
        public RoundDto Round { get; private set; }
        public PlayHandResultDto(PokerHandDto pokerHand, List<CardDto> scoredCards, List<CardDto> cardsPlayed, List<CardDto> newCards, RoundDto round)
        {
            PokerHand = pokerHand;
            ScoredCards = scoredCards;
            CardsPlayed = cardsPlayed;
            NewCards = newCards;
            Round = round;
        }
    }
}