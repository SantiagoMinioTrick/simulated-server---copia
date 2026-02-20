using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking
{
    public class Boss_BlindRooster : Boss
    {
        public Boss_BlindRooster(string id, string bossName, string bossDesc, int reward, float scoreMultiplier) : base(id, bossName, bossDesc, reward, scoreMultiplier)
        {
        }

        protected override List<CardDto> OnBossEfect_DealNewCards(List<CardDto> newCards)
        {
            return newCards;
        }

        protected override void OnBossEfect_EndBreach()
        {
        }

        protected override List<CardDto> OnBossEfect_FirstHand(List<CardDto> firstHand)
        {
            for (int i = 0; i < firstHand.Count; i++)
            {
                firstHand[i] = new CardDto(firstHand[i].ID, firstHand[i].Symbol, firstHand[i].Value, firstHand[i].Score, true);
            }

            return firstHand;
        }

        protected override void OnBossEfect_StartBreach()
        {
        }

        protected override PokerHandDto OnBossEffect_PlayPokerHand(PokerHandDto playedPokerHand)
        {
            return playedPokerHand;
        }
    }
}
