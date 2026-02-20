using SimulatedBE.Networking.DTOs;
using SimulatedBE.Networking.Jokers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimulatedBE.Networking
{
    public class ShopManager
    {
        public readonly static Dictionary<JokerRarity, float> jokerAppearChances = new Dictionary<JokerRarity, float>()
        {
            {JokerRarity.Common, 1f },
            {JokerRarity.Rare, 0.7f },
            {JokerRarity.Epic, 0.3f }
        };

        public const string SugarTownPoolName = "SugarTownPool";
        public const string FarmCardsPoolName = "FarmCardsPool";
        public const string JokerPoolName = "JokerPool";
        public const string VoucherPoolName = "VoucherPool";
        public const string PackPoolName = "PackPool";

        private IPool _sugarTownPool;
        private IPool _farmCardsPool;
        private IPool _jokerPool;
        private IPool _genericPool;
        private IPool _voucherPool;
        private IPool _packPool;

        private int _genericItemsAmmount = 2;
        private int _voucherItemsAmmount = 1;
        private int _packItemsAmmount = 2;
        private int _baseRerollCost = 5;
        private bool _canBuyVoucher;


        public ShopManager(GameRules gameRules)
        {
            _canBuyVoucher = true;
            _sugarTownPool = new Shop_Pool(SugarTownPoolName, JSONFilesNames.Preset_Shop_Planets);
            _farmCardsPool = new Shop_Pool(FarmCardsPoolName, JSONFilesNames.Preset_Shop_FarmCards);
            _jokerPool = new Shop_Pool(JokerPoolName, JSONFilesNames.Preset_Shop_Jokers);
            _voucherPool = new Shop_Pool(VoucherPoolName, JSONFilesNames.Preset_Shop_Vouchers);
            _packPool = new Shop_Pool(PackPoolName, JSONFilesNames.Preset_Shop_Packs);
            IPool poolOfConsumables = new Shop_PoolOfPools("ConsumablesPool", new ShopPoolDto(_sugarTownPool, 0.5f), new ShopPoolDto(_farmCardsPool, 1));
            _genericPool = new Shop_PoolOfPools("GenericPool", new ShopPoolDto(_jokerPool, 1.5f), new ShopPoolDto(poolOfConsumables, 1));
            gameRules.OnNewAnte += OnNewAnte;
        }

        private void OnNewAnte() => _canBuyVoucher = true;

        public ShopDto GetRandomShopItems(JokerManager jokerManager)
        {
            var genericItems = GetItemsOfPool(_genericPool, _genericItemsAmmount);
            for (int i = 0; i < genericItems.Count; i++)
            {
                if (genericItems[i].Type != ItemType.Joker) continue;

                var rarity = GetRandomRarity();

                var modifiedItem = genericItems[i].SetRarity(rarity.ToString());
                genericItems[i] = modifiedItem.SetDescription(jokerManager.SetDescriptionValues(modifiedItem));
            }
            
            var voucherItems = _canBuyVoucher ? GetItemsOfPool(_voucherPool, _voucherItemsAmmount) : new List<ItemDto>();
            var packItems = GetItemsOfPool(_packPool, _packItemsAmmount);

            return new ShopDto(genericItems, voucherItems, packItems, _baseRerollCost);
        }

        private JokerRarity GetRandomRarity()
        {
            var rarity = JokerRarity.Common;
            float maxAppearChance = jokerAppearChances.Sum(x => x.Value);
            float random = Random.Range(0, maxAppearChance);

            foreach (var item in jokerAppearChances)
            {
                random -= item.Value;

                if(random <= 0)
                {
                    rarity = item.Key;
                    break;
                }
            }

            return rarity;
        }

        private List<ItemDto> GetItemsOfPool(IPool pool, int ammountOfItems)
        {
            List<ShopItemDto> result = new List<ShopItemDto>();

            for (int i = 0; i < ammountOfItems; i++)
            {
                var temp = pool.GetRandomItem(result);

                if (string.IsNullOrEmpty(temp.ID))
                    break;

                result.Add(temp);
            }

            return result.Select(x => x.Item.CopyItem()).ToList();
        }

        public ShopDto RemoveShopDto(ShopDto shopToBeRemoved, ItemDto item, IJsonFacade facade)
        {
            if (item.Type == ItemType.Voucher)
                return BuyVoucher(item, facade, shopToBeRemoved);
            else if (item.Type == ItemType.Booster)
                return BuyBoosterPack(item, facade, shopToBeRemoved);
            else
                return BuyGenericItem(item, facade, shopToBeRemoved);
        }

        private ShopDto BuyGenericItem(ItemDto item, IJsonFacade facade, ShopDto shopToBeRemoved)
        {
            var inventory = facade.CurrentInventory;
            for (int i = 0; i < shopToBeRemoved.GenericItems.Count; i++)
            {
                if (shopToBeRemoved.GenericItems[i].ItemName != item.ItemName)
                    continue;

                shopToBeRemoved.GenericItems.RemoveAt(i);
                if (item.Type == ItemType.Joker)
                    inventory.Jokers.Add(item);
                else
                    inventory.Consumables.Add(item);

                facade.CurrentInventory = inventory;
                return shopToBeRemoved;
            }

            return shopToBeRemoved;
        }

        private ShopDto BuyVoucher(ItemDto item, IJsonFacade facade, ShopDto shopToBeRemoved)
        {
            var inventory = facade.CurrentInventory;
            _canBuyVoucher = false;
            for (int i = 0; i < shopToBeRemoved.Vouchers.Count; i++)
            {
                if (shopToBeRemoved.Vouchers[i].ItemName != item.ItemName)
                    continue;
                shopToBeRemoved.Vouchers.RemoveAt(i);
                inventory.Vouchers.Add(item);
                facade.CurrentInventory = inventory;
                return shopToBeRemoved;
            }
            return shopToBeRemoved;
        }

        private ShopDto BuyBoosterPack(ItemDto item, IJsonFacade facade, ShopDto shopToBeRemoved)
        {
            var inventory = facade.CurrentInventory;
            for (int i = 0; i < shopToBeRemoved.Packagings.Count; i++)
            {
                if (shopToBeRemoved.Packagings[i].ItemName != item.ItemName)
                    continue;
                shopToBeRemoved.Packagings.RemoveAt(i);
                facade.CurrentInventory = inventory;
                return shopToBeRemoved;
            }
            return shopToBeRemoved;
        }

    }
}
