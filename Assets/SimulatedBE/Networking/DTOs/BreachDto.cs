namespace SimulatedBE.Networking.DTOs
{
    public struct BreachDto
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public BreachState BreachState { get; private set; }
        public int BasicReward { get; private set; }    
        public BreachType BreachType { get; private set; }
        public bool Current { get; private set; }
        public long ObjectiveScore { get; private set; }
        public TagDto Tag { get; private set; }
        public BreachDto(string name,string description,int basicReward,BreachState breachState,BreachType breachType,bool current, long objectiveScore, TagDto tag)
        {
            Name = name;
            Description = description;
            BasicReward = basicReward;
            BreachType = breachType;
            BreachState = breachState;
            Current = current;
            ObjectiveScore = objectiveScore;
            Tag = tag;
        }
    }
}