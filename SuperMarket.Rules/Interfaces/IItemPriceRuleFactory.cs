using System.Collections.Generic;

namespace SuperMarket.Rules.Interfaces
{
    public interface IItemPriceRuleFactory
    {
        IReadOnlyList<IItemPriceRule> GetAllItemPriceRules();
    }
}