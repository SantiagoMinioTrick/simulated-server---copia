namespace SimulatedBE.Networking.Tags
{
    public interface ITag
    {
        void ApplyTag(TagManager tagManager);

        string GetDynamicDescription();
    }
}
