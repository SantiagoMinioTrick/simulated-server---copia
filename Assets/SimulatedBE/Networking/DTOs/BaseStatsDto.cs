using System;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public struct BaseStatsDto
    {

        public int AmountOfCardsInHand { get; private set; }
        public int AmmountOfPlayHands { get; private set; }
        public int AmmountOfDiscards { get; private set; }
        public string SmallBreachName { get; private set; }
        public string BigBreachName { get; private set; }
        public int BreachBaseScore { get; private set; }
        public int BetsToWinGame { get; private set; }
        public int JokerSlotsAmmount { get; private set; }
        public int ConsumableSlotsAmmount { get; private set; }
        public float[] AnteMultipliers { get; private set; }

        public BaseStatsDto(int amountOfCardsInHand, int ammountOfPlayHands, int ammountOfDiscards, string smallBreachName, string bigBreachName, int breachBaseScore, int betsToWinGame, int jokerSlotsAmmount, int consumableSlotsAmmount, float[] anteMultipliers)
        {
            AmountOfCardsInHand = amountOfCardsInHand;
            AmmountOfPlayHands = ammountOfPlayHands;
            AmmountOfDiscards = ammountOfDiscards;
            SmallBreachName = smallBreachName;
            BigBreachName = bigBreachName;
            BreachBaseScore = breachBaseScore;
            BetsToWinGame = betsToWinGame;
            JokerSlotsAmmount = jokerSlotsAmmount;
            ConsumableSlotsAmmount = consumableSlotsAmmount;
            AnteMultipliers = anteMultipliers;
        }
    }
}
