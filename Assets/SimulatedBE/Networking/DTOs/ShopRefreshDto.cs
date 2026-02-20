
namespace SimulatedBE.Networking.DTOs
{
    public struct ShopRefreshDto
    {
        public ShopDto Shop { get; private set; }
        public int UsedCurrency { get; private set; }
        public bool Successful { get; private set; }

        public ShopRefreshDto(ShopDto shop, int usedCurrency, bool successfull)
        {
            Shop = shop;
            UsedCurrency = usedCurrency;
            Successful = successfull;
        }
    }
}
