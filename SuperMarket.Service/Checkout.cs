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

        public Checkout(ICheckoutFactory checkoutFactory)
        {
            _scannedItems = new List<string>();
            _itemPriceRules = checkoutFactory.CreateCheckout();
        }

        public List<Product> DisplayAvailableItems()
        {
            _availableItems = new List<Product>
            {
                new Product{
                Sku = "A99",
                Description = "Apple",
                UnitPrice = 0.50m
                },
                new Product
                {
                    Sku = "B15",
                    Description = "Biscuit",
                    UnitPrice = 0.30m
                }
                ,
                new Product
                {
                    Sku = "C40",
                    Description = "Coffee",
                    UnitPrice = 1.80m
                }
                ,
                new Product
                {
                    Sku = "T23",
                    Description = "Tissues",
                    UnitPrice = 0.99m
                }
            };

            return _availableItems;
        }

        public void ScanItem(string item)
        {
            if(_availableItems.Count(x => x.Sku==item) > 0)
                _scannedItems.Add(item);
        }

        public List<string> BasketItems()
        {
            string basketItems = @"SKU: {0} - Description: {1} - Unit Price: {2}";


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