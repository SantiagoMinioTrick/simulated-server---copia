using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking
{
    public class Database
    {
        public int InitialRound;
        public int CardsInHand;
        public int NumberOfCardsPlayed;
        public int DiscardedCards;
        public long BestHandScore;
        public int CurrentInitialBet;
        public int temporalTotalReward;
        public int PlayHandsAmmount;
        public int DiscardsAmmount;
        public long CurrentID = 0;

        public int CardsPurchased;
        public int Renewals;
        public int Discoveries;
        public PokerHandDto MostPlayedHand = new PokerHandDto();
        public PokerHandDto LastHandPlayed = new PokerHandDto();

        public List<TagDto> currentTags = new List<TagDto>();

        public Database(int playHandsAmmount, int discardsAmmount)
        {
            PlayHandsAmmount = playHandsAmmount;
            DiscardsAmmount = discardsAmmount;
        }

        public long GetNewID()
        {
            var current = CurrentID;
            CurrentID += 1;
            return current;
        }
    }
}