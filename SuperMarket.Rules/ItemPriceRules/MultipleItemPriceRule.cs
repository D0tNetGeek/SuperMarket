using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;

namespace SuperMarket.Rules.ItemPriceRules
{
    public class MultipleItemPriceRule:IItemPriceRule
    {
        private readonly char _itemSku;
        private readonly decimal _itemBundlePrice;
        private readonly decimal _itemsInBundle;

        public MultipleItemPriceRule(char itemSku, decimal itemBundlePrice, decimal itemsInBundle)
        {
            _itemSku = itemSku;
            _itemBundlePrice = itemBundlePrice;
            _itemsInBundle = itemsInBundle;
        }

        public decimal CalculatePrice(List<char> itemsLeft)
        {
            var itemCount = itemsLeft.Count(x => x == _itemSku);

            var itemBundles = (int) (itemCount / _itemsInBundle);

            var bundledItems = itemBundles * _itemsInBundle;

            for (int i=0; i< bundledItems;i++)
            {
                itemsLeft.Remove(_itemSku);
            }

            var price = itemBundles * _itemBundlePrice;

            return price;
        }
    }
}
