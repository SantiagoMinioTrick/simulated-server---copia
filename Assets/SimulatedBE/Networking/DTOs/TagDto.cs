namespace SimulatedBE.Networking.DTOs
{
    public struct TagDto
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string DynamicDescription { get; private set; }
        public string HandState { get; private set; }
    
        public TagDto(string iD, string name, string description, string dynamicDescription, string handState)
        {
            ID = iD;
            Name = name;
            Description = description;
            DynamicDescription = dynamicDescription;
            HandState = handState;
        }
    }
}
