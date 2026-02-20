using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    public struct DiscardCardResultDto
    {
        public List<CardDto> NewCards { get; private set; }
        public int AmountOfDiscardsRemaining { get; private set; }
        public DiscardCardResultDto(List<CardDto> newCards,int amountOfDiscardsRemaining)
        {
            NewCards = newCards;
            AmountOfDiscardsRemaining = amountOfDiscardsRemaining;
        }
    }
}