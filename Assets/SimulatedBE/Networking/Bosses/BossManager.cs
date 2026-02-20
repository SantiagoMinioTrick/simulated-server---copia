using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SimulatedBE.Networking
{
    public class BossManager
    {
        private Dictionary<string, Boss> _allBosses = new Dictionary<string, Boss>();
        private Boss _currentBoss;

        public BossManager()
        {
            _allBosses.Add("TheBlindRooster", new Boss_BlindRooster("TheBlindRooster", "The Blind Rooster", "The first hand is dealt face down.", 5, 2));
        }

        public List<CardDto> FirstHandEffect(List<CardDto> firstHand)
        {
            return _currentBoss.BossEfect_FirstHand(firstHand);
        }

        public List<CardDto> OnDealNewCardsEffect(List<CardDto> newCards)
        {
            return _currentBoss.BossEfect_DealNewCards(newCards);
        }

        public PokerHandDto OnPlayHandEffect(PokerHandDto pokerHand)
        {
            return _currentBoss.BossEffect_PlayPokerHand(pokerHand);
        }

        public void SetBoss(string bossName)
        {
            _currentBoss = GetBossByID(bossName);
            _currentBoss.BossEfect_StartBreach();
        }

        public bool IsBossActivated() => _currentBoss != null;

        public Boss GetRandomBoss()
        {
            return _allBosses.ToArray()[UnityEngine.Random.Range(0, _allBosses.Count)].Value;
        }

        private Boss GetBossByID(string id)
        {
            if (_allBosses.TryGetValue(id, out Boss boss))
            {
                return boss;
            }
            else
            {
                Debug.Log("The Boss ID doesn't exist.");
                return null;
            }
        }
    }
}
