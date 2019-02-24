using System.Collections.Generic;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service
{
    public class Checkout : ICheckout
    {
        private readonly IEnumerable<IItemPriceRule> _itemPriceRules;

        private readonly List<char> _scannedItems;

        public Checkout(IEnumerable<IItemPriceRule> itemPriceRules)
        {
            _itemPriceRules = itemPriceRules;
            _scannedItems = new List<char>();
        }

        public decimal CalculateTotalPrice()
        {
            throw new System.NotImplementedException();
        }

        public void ScanItem(char item)
        {
            throw new System.NotImplementedException();
        }
    }
}