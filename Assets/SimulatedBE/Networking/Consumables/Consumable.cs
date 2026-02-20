using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking
{
    public interface IConsumable
    {
        List<CardDto> ApplyConsumable(List<CardDto> selectedCards);
    }
}
