using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service.Factory
{
    public class CheckoutFactory : ICheckoutFactory
    {
        private readonly IItemPriceRuleFactory _itemPriceRuleFactory;

        public CheckoutFactory(IItemPriceRuleFactory itemPriceRuleFactory)
        {
            _itemPriceRuleFactory = itemPriceRuleFactory;
        }

        public List<IItemPriceRule> CreateCheckout()
        {
            var itemPriceRules = _itemPriceRuleFactory.GetAllItemPriceRules();

            return itemPriceRules.ToList();
        }

        public List<Product> GetAvailableItems()
        {
            var availableItems = _itemPriceRuleFactory.GetAvailableItems();

            return availableItems.Select(x => new Product {Sku = x.Key, UnitPrice = x.Value}).ToList();
        }
    }
}