using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Vouchers
{
    public interface IVoucher
    {
        InstructionDto ApplyVoucher(InstructionDto baseInstruction);
    }
}
