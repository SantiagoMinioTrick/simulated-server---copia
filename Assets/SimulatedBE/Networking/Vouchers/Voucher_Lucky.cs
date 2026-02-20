using SimulatedBE.Networking.DTOs;

namespace SimulatedBE.Networking.Vouchers
{
    public class Voucher_Lucky : IVoucher
    {
        int treasureBonus;
        IJsonFacade facade;

        public Voucher_Lucky(int treasureBonus, IJsonFacade facade)
        {
            this.treasureBonus = treasureBonus;
            this.facade = facade;
        }

        public InstructionDto ApplyVoucher(InstructionDto baseInstrucion)
        {
            facade.CurrentInventory = facade.CurrentInventory.AddTreasure(treasureBonus);
            return new InstructionDto(baseInstrucion.KeyValue, baseInstrucion.ActionItem, 0, 0, treasureBonus, facade.CurrentInventory, baseInstrucion.UpdatedRound);
        }
    }
}