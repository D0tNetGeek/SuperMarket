using System;
using System.Collections.Generic;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service
{
    public class Checkout : ICheckout
    {
        private readonly IEnumerable<IItemPriceRule> _itemPriceRules;

        private readonly List<string> _scannedItems;

        public Checkout(IEnumerable<IItemPriceRule> itemPriceRules)
        {
            _itemPriceRules = itemPriceRules;
            _scannedItems = new List<string>();
        }

        public void ScanItem(string item)
        {
            _scannedItems.Add(item);
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = decimal.Zero;

            var itemsLeft = new List<string>(_scannedItems);

            foreach (var itemPriceRule in _itemPriceRules)
            {
                total += itemPriceRule.CalculatePrice(itemsLeft);
            }

            if(itemsLeft.Count !=0)
                throw new ApplicationException("Invalid items: "+string.Join(", ",itemsLeft));

            return total;
        }
    }
}