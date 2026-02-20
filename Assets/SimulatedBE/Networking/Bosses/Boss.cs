using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using UnityEngine;

namespace SimulatedBE.Networking
{
    public abstract class Boss
    {
        [SerializeField] string id = "";

        [SerializeField] string bossName = "";
        [SerializeField] string bossDesc = "";
        [SerializeField] int reward = 5;
        [SerializeField] float scoreMultiplier = 2f;

        protected Boss(string id, string bossName, string bossDesc, int reward, float scoreMultiplier)
        {
            this.id = id;
            this.bossName = bossName;
            this.bossDesc = bossDesc;
            this.reward = reward;
            this.scoreMultiplier = scoreMultiplier;
        }

        public string ID { get { return id; } }


        public BreachDto GetBossBreach(BreachState state, float basicScore)
        {
            return new BreachDto(bossName, bossDesc, reward, state, BreachType.Boss, state == BreachState.Current, (long)(basicScore * scoreMultiplier), new TagDto());
        }

        public void BossEfect_StartBreach()
        {
            OnBossEfect_StartBreach();
        }

        public void BossEfect_EndBreach()
        {
            OnBossEfect_EndBreach();
        }

        public List<CardDto> BossEfect_FirstHand(List<CardDto> firstHand)
        {
            return OnBossEfect_FirstHand(firstHand);
        }

        public List<CardDto> BossEfect_DealNewCards(List<CardDto> newCards)
        {
            return OnBossEfect_DealNewCards(newCards);
        }

        public PokerHandDto BossEffect_PlayPokerHand(PokerHandDto playedPokerHand)
        {
            return OnBossEffect_PlayPokerHand(playedPokerHand);
        }


        protected abstract void OnBossEfect_StartBreach();
        protected abstract void OnBossEfect_EndBreach();
        protected abstract List<CardDto> OnBossEfect_FirstHand(List<CardDto> firstHand);
        protected abstract List<CardDto> OnBossEfect_DealNewCards(List<CardDto> newCards);
        protected abstract PokerHandDto OnBossEffect_PlayPokerHand(PokerHandDto playedPokerHand);
    }
}
