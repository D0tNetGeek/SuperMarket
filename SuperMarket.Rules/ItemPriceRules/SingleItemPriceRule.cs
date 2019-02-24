using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;

namespace SuperMarket.Rules.ItemPriceRules
{
    public class SingleItemPriceRule : IItemPriceRule
    {
        private readonly string _itemSku;
        private readonly decimal _itemPrice;

        public SingleItemPriceRule(string itemSku, decimal itemPrice)
        {
            _itemSku = itemSku;
            _itemPrice = itemPrice;
        }

        public decimal CalculatePrice(List<string> itemsLeft)
        {
            decimal itemCount = itemsLeft.Count(x => x == _itemSku);

            decimal itemPrice = itemCount * _itemPrice;

            itemsLeft.RemoveAll(x => x == _itemSku);

            return itemPrice;
        }
    }
}
