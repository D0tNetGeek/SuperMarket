using System;
using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service
{
    public class Checkout : ICheckout
    {
        private readonly IEnumerable<IItemPriceRule> _itemPriceRules;

        private readonly List<string> _scannedItems;

        private List<Product> _availableItems;

        private readonly ICheckoutFactory _checkoutFactory;

        public Checkout(ICheckoutFactory checkoutFactory)
        {
            _scannedItems = new List<string>();
            _checkoutFactory = checkoutFactory;
            _itemPriceRules = _checkoutFactory.CreateCheckout();
        }

        public List<Product> DisplayAvailableItems()
        {
            _availableItems = _checkoutFactory.GetAvailableItems();
           
            return _availableItems;
        }

        public void ScanItem(string item)
        {
            if(_availableItems.Count(x => x.Sku==item) > 0)
                _scannedItems.Add(item);
        }

        public List<string> BasketItems()
        {
            return _scannedItems;
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
                throw new ApplicationException("Invalid items: "+string.Join(", ", itemsLeft));

            return total;
        }
    }
}