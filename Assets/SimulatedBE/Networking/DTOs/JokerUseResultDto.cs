using System.Collections.Generic;

namespace SimulatedBE.Networking.DTOs
{
    public struct JokerUseResultDto
    {
        public List<InstructionDto> JokerInstructions { get; private set; }


        public JokerUseResultDto(List<InstructionDto> jokerInstructions)
        {
            JokerInstructions = jokerInstructions;
        }
    }
}