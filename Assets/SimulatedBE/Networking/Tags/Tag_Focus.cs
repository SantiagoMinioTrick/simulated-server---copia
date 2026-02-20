namespace SimulatedBE.Networking.Tags
{
    public class Tag_Focus : ITag
    {
        private readonly IJsonFacade _jsonFacade;
        private TagManager _tagManager;
        
        public Tag_Focus(IJsonFacade jsonFacade, TagManager tagManager)
        {
            _jsonFacade = jsonFacade;
            _tagManager = tagManager;
        }

        public void ApplyTag(TagManager tagManager)
        {
            OnDrawCards();
        }

        void OnDrawCards()
        {
            _tagManager.DrawCards(_jsonFacade.Database.currentTags.Count);
            UnityEngine.Debug.Log("Cards Drawed");
        }

        public string GetDynamicDescription() => "(You will get " + (_jsonFacade.Database.currentTags.Count + 1).ToString() + " extra cards)";
    }
}