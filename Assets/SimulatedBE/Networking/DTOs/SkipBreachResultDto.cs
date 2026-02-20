namespace SimulatedBE.Networking.DTOs
{
    public struct SkipBreachResultDto
    {
        public TagDto TagDto { get; private set; }

        public SkipBreachResultDto(TagDto tagDto)
        {
            TagDto = tagDto;
        }
        
    }
}