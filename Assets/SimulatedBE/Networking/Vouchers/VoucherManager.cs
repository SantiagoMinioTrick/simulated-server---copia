using SimulatedBE.Networking.DTOs;
using System.Collections.Generic;

namespace SimulatedBE.Networking.Vouchers
{
    public class VoucherManager
    {
        Dictionary<string, IVoucher> vouchers = new Dictionary<string, IVoucher>();
        IJsonFacade jsonFacade;

        public VoucherManager(IJsonFacade facade)
        {
            jsonFacade = facade;
            vouchers.Add("Lucky", new Voucher_Lucky(5, jsonFacade));
        }

        public InstructionDto ApplyVoucher(string ID)
        {
            InstructionDto instruction = new InstructionDto(ID, null, 0,0,0,jsonFacade.CurrentInventory, jsonFacade.RoundData);
            if(vouchers.TryGetValue(ID, out var voucher))
                instruction = voucher.ApplyVoucher(instruction);

            return instruction;
        }
    }
}
