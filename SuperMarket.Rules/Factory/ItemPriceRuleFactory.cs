using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Rules.ItemPriceRules;

namespace SuperMarket.Rules.Factory
{
    public class ItemPriceRuleFactory
    {
        public IReadOnlyList<IItemPriceRule> GetAllItemPriceRules()
        {
            var rules = new List<IItemPriceRule>
            {
                CreateThreeAItemPriceRule(),
                CreateTwoBItemPriceRule()
            };

            rules.AddRange(CreateSingleItemPriceRules());

            return rules;
        }

        private static IEnumerable<IItemPriceRule> CreateSingleItemPriceRules()
        {
            return ItemCodePriceMap.Select(map => new SingleItemPriceRule(map.Key, map.Value));
        }

        private IItemPriceRule CreateThreeAItemPriceRule()
        {
            return new MultipleItemPriceRule("A99", 1.30m, 3);
        }

        private IItemPriceRule CreateTwoBItemPriceRule()
        {
            return new MultipleItemPriceRule("B15", 0.45m, 2);
        }

        private static readonly IReadOnlyDictionary<string, decimal> ItemCodePriceMap = new Dictionary<string, decimal>(){
            {"A99", 0.50m},
            {"B15", 0.30m},
            {"C40", 1.80m},
            {"T23", 0.99m}
        };
    }
}