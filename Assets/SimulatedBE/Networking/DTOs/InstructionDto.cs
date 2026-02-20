namespace SimulatedBE.Networking.DTOs
{
    public struct InstructionDto
    {
        public string KeyValue { get; private set; }
        public ItemDto ActionItem { get; private set; }
        public long AddedScore { get; private set; }
        public long AddedMultiplier { get; private set; }
        public int UpdatedTreasure { get; private set; }
        public InventoryDto UpdatedInventory { get; private set; }
        public RoundDto UpdatedRound { get; private set; }
        public bool Multiply { get; private set; }
        //etc

        public InstructionDto(string keyValue, ItemDto actionItem, long addedScore, long addedMultiplier, int updatedTreasure, InventoryDto updatedInventory, RoundDto updatedRound, bool multiply = false)
        {
            KeyValue = keyValue;
            ActionItem = actionItem;
            AddedScore = addedScore;
            AddedMultiplier = addedMultiplier;
            UpdatedTreasure = updatedTreasure;
            UpdatedInventory = updatedInventory;
            UpdatedRound = updatedRound;
            Multiply = multiply;
        }

        public InstructionDto Additive(InstructionDto instruction)
        {
            if (Multiply)
            {
                AddedScore *= instruction.AddedScore;
                AddedMultiplier *= instruction.AddedMultiplier;
            }
            else
            {
                AddedScore += instruction.AddedScore;
                AddedMultiplier += instruction.AddedMultiplier;
            }
            UpdatedTreasure += instruction.UpdatedTreasure;
            UpdatedInventory = instruction.UpdatedInventory;
            UpdatedRound = instruction.UpdatedRound;

            return this;
        }

        public InstructionDto SetItem(ItemDto item)
        {
            ActionItem = item;
            return this;
        }
    }
}