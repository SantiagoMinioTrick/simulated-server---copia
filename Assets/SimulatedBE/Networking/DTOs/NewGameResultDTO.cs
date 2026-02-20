namespace SimulatedBE.Networking.DTOs
{
    public struct NewGameResultDto
    {
        public DeckDto DeckDto { get; private set; }
        public NewGameResultDto(DeckDto deckDto)
        {
            DeckDto = deckDto;
        }
    }
}