using System.Collections.Generic;
using System.Linq;
using SuperMarket.Repository;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service.Factory
{
    public class CheckoutFactory : ICheckoutFactory
    {
        private readonly IItemPriceRuleFactory _itemPriceRuleFactory;
        private readonly ISuperMarketData _superMarketRepo;

        public CheckoutFactory(IItemPriceRuleFactory itemPriceRuleFactory, ISuperMarketData superMarketRepo)
        {
            _itemPriceRuleFactory = itemPriceRuleFactory;
            _superMarketRepo = superMarketRepo;
        }

        public List<IItemPriceRule> CreateCheckout()
        {
            var itemPriceRules = _itemPriceRuleFactory.GetAllItemPriceRules();

            return itemPriceRules.ToList();
        }

        public List<Product> GetAvailableItems()
        {
            var availableItems = _superMarketRepo.DisplayAvailableItems();

            return availableItems.Select(x => new Product {Sku = x.Sku, Description = x.Description, UnitPrice = x.UnitPrice}).ToList();
        }
    }
}