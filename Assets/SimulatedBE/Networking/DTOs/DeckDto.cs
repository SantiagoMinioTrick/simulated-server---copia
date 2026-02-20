using System;
using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    [Serializable]
    public struct DeckDto
    {
        public string DeckName { get; private set; }
        public string DeckDescription { get; private set; }
        public List<CardDto> Cards { get; private set; }

        public DeckDto(string deckName,string deckDescription,List<CardDto> cards)
        {
            DeckName = deckName;
            DeckDescription = deckDescription;
            Cards = cards;
        }
    }
}


