using System.Collections.Generic;

namespace SuperMarket.Rules.Interfaces
{
    public interface IItemPriceRule
    {
        decimal CalculatePrice(List<string> itemsLeft);
    }
}