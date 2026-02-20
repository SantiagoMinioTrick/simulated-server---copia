using System;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public struct PokerHandDto
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public ScoreDto Score { get; private set; }
        public int AmountOfTimesPlayed { get; private set; }

        public PokerHandDto(string name, int level, ScoreDto score, int amountOfTimesPlayed)
        {
            Name = name;
            Level = level;
            Score = score;
            AmountOfTimesPlayed = amountOfTimesPlayed;
        }
    }
}