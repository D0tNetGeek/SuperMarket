using System.Collections.Generic;
using System.Linq;
using SuperMarket.Repository;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Rules.ItemPriceRules;
using SuperMarket.Rules.Models;

namespace SuperMarket.Rules.Factory
{
    public class ItemPriceRuleFactory : IItemPriceRuleFactory
    {
        private readonly List<Product> _products;
        private readonly ISuperMarketData _superMarketRepo;
        private static IReadOnlyDictionary<string, decimal> _itemCodePriceMap;

        public ItemPriceRuleFactory(ISuperMarketData superMarketRepo)
        {
            _superMarketRepo = superMarketRepo;
            _itemCodePriceMap = _superMarketRepo.DisplayAvailableItems().ToDictionary(x=>x.Sku, x=>x.UnitPrice);
        }

        public IReadOnlyList<IItemPriceRule> GetAllItemPriceRules()
        {
            var rules = new List<IItemPriceRule>
            {
                CreateThreeAItemSpecialOfferRule(),
                CreateTwoBItemSpecialOfferRule()
            };

            rules.AddRange(CreateSingleItemPriceRules());

            return rules;
        }

        public IReadOnlyDictionary<string, decimal> GetAvailableItems()
        {
            return _itemCodePriceMap;
        }

        private static IEnumerable<IItemPriceRule> CreateSingleItemPriceRules()
        {
            return _itemCodePriceMap.Select(map => new SingleItemPriceRule(map.Key, map.Value));
        }

        private IItemPriceRule CreateThreeAItemSpecialOfferRule()
        {
            return new MultipleItemPriceRule("A99", 1.30m, 3);
        }

        private IItemPriceRule CreateTwoBItemSpecialOfferRule()
        {
            return new MultipleItemPriceRule("B15", 0.45m, 2);
        }
    }
}