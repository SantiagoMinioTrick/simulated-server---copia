namespace SimulatedBE.Networking.Tags
{
    public class Tag_Momentum : ITag
    {
        private IJsonFacade _facade;
        private int _goldAmmount;

        public Tag_Momentum(IJsonFacade jsonFacade, int goldAmmount)
        {
            _facade = jsonFacade;
            _goldAmmount = goldAmmount;
        }

        public void ApplyTag(TagManager tagManager)
        {
            var inventory = _facade.CurrentInventory;
            _facade.CurrentInventory = inventory.AddTreasure(_goldAmmount * _facade.Database.currentTags.Count);
        }

        public string GetDynamicDescription()
        {
            return "(You will get $" + (_goldAmmount * (_facade.Database.currentTags.Count + 1)) + ")";
        }
    }
}