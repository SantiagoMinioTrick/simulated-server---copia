namespace SimulatedBE.Networking.Tags
{
    public class Tag_Patience : ITag
    {
        public void ApplyTag(TagManager tagManager) => tagManager.SetTagMultiplier(1.5f);

        public string GetDynamicDescription() => "";
    }
}