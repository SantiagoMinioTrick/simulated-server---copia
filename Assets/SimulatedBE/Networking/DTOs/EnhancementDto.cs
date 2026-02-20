namespace SimulatedBE.Networking.DTOs
{
    public struct EnhancementDto
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public EnhancementDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}