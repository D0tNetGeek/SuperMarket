using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;

namespace SuperMarket.Rules.ItemPriceRules
{
    public class SingleItemPriceRule : IItemPriceRule
    {
        private readonly char _itemSku;
        private readonly decimal _itemPrice;

        public SingleItemPriceRule(char itemSku, decimal itemPrice)
        {
            _itemSku = itemSku;
            _itemPrice = itemPrice;
        }

        public decimal CalculatePrice(List<char> itemsLeft)
        {
            decimal itemCount = itemsLeft.Count(x => x == _itemSku);

            decimal itemPrice = itemCount * _itemPrice;

            itemsLeft.RemoveAll(x => x == _itemSku);

            return itemPrice;
        }
    }
}
