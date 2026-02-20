namespace SimulatedBE.Networking.DTOs
{
    public struct ModifierDto
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public ModifierDto(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
