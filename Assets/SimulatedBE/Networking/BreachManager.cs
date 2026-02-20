using SimulatedBE.Networking.DTOs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimulatedBE.Networking
{
    public class BreachManager
    {
        public BreachDto GetCurrentBreach(List<BreachDto> breaches)
        {
            var currentBreach = new BreachDto();
            foreach (var t in breaches)
            {
                if (t.Current)
                {
                    currentBreach = t;
                    break;
                }
            }

            return currentBreach;
        }

        public Tuple<BreachDto, BreachDto, List<BreachDto>> SetNewCurrentBreach(List<BreachDto> breaches, bool skip)
        {
            var currentBreach = new BreachDto();
            var newBreach = new BreachDto();
            int nextBreach = -1;

            for (int i = 0; i < breaches.Count; i++)
            {
                if (breaches[i].Current)
                {
                    breaches[i] = new BreachDto(breaches[i].Name, breaches[i].Description, breaches[i].BasicReward,
                        skip ? BreachState.Skipped : BreachState.Defeated, breaches[i].BreachType, false,
                        breaches[i].ObjectiveScore, breaches[i].Tag);
                    currentBreach = breaches[i];
                    nextBreach = i + 1;
                    break;
                }
            }

            if (nextBreach >= 0 && nextBreach < breaches.Count)
            {
                breaches[nextBreach] = new BreachDto(breaches[nextBreach].Name, breaches[nextBreach].Description,
                    breaches[nextBreach].BasicReward, BreachState.Current, breaches[nextBreach].BreachType, true,
                    breaches[nextBreach].ObjectiveScore, breaches[nextBreach].Tag);
                newBreach = breaches[nextBreach];
            }


            return new Tuple<BreachDto, BreachDto, List<BreachDto>>(currentBreach, newBreach, breaches);
        }

        public List<BreachDto> CreateNewBreaches(BaseStatsDto baseData, BossManager bossManager, int currentBet)
        {
            var tags = JsonSerializer.ReadJsonFiles<TagDto>(Paths.Tags);
            var breaches = new List<BreachDto>();
            float multiplier = currentBet >= baseData.AnteMultipliers.Length ? baseData.AnteMultipliers[baseData.AnteMultipliers.Length - 1] :
                                   baseData.AnteMultipliers[currentBet];

            int baseBreachScore = Mathf.RoundToInt(baseData.BreachBaseScore * multiplier);
            Debug.Log("Base breach score: " + baseBreachScore + "/" + multiplier);
            var randomTag = tags[UnityEngine.Random.Range(0, tags.Count)];
            var smallBreach = new BreachDto(baseData.SmallBreachName, "Small Breach", 3, BreachState.Current,
                BreachType.SmallBlind, true, baseBreachScore, randomTag);
            randomTag = tags[UnityEngine.Random.Range(0, tags.Count)];
            var bigBreach = new BreachDto(baseData.BigBreachName, "Big Breach", 4, BreachState.Locked,
                BreachType.BigBlind, false, (long)(baseBreachScore * 1.5f), randomTag);

            var currentBoss = bossManager.GetRandomBoss();

            var bossBreach = currentBoss.GetBossBreach(BreachState.Locked, baseBreachScore);

            breaches.Add(smallBreach);
            breaches.Add(bigBreach);
            breaches.Add(bossBreach);

            return breaches;
        }
    }
}
