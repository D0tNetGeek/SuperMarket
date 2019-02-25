using System;
using System.Collections.Generic;
using System.Linq;
using SuperMarket.Repository;
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
        private readonly ISuperMarketData _superMarketRepo;

        public Checkout(ICheckoutFactory checkoutFactory, ISuperMarketData superMarketRepo)
        {
            _scannedItems = new List<string>();
            _checkoutFactory = checkoutFactory;
            _superMarketRepo = superMarketRepo;

            //_itemPriceRules = itemPriceRules; //_checkoutFactory.CreateCheckout();
        }

        public List<Product> DisplayAvailableItems()
        {
            _availableItems = _superMarketRepo.DisplayAvailableItems().Select(x=>new Product
            {
                Sku = x.Sku,
                Description = x.Description,
                UnitPrice = x.UnitPrice
            }).ToList();
           
            return _availableItems;
        }

        public void ScanItem(string item)
        {
            //if(_availableItems.Count(x => x.Sku==item) > 0)
                _scannedItems.Add(item);
        }

        public void AvailableItems(string item)
        {
            _availableItems.Add(new Product {Sku = item});
        }

        public List<string> BasketItems()
        {
            return _scannedItems;
        }

        public decimal CalculateTotalPrice()
        {
            decimal total = decimal.Zero;

            var itemsLeft = new List<string>(_scannedItems);

            foreach (var itemPriceRule in  _checkoutFactory.CreateCheckout()) //; _itemPriceRules)
            {
                total += itemPriceRule.CalculatePrice(itemsLeft);
            }

            if(itemsLeft.Count !=0)
                throw new ApplicationException("Invalid items: "+string.Join(", ", itemsLeft));

            return total;
        }
    }
}