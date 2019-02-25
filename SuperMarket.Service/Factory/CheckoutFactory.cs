using System.Collections.Generic;
using System.Linq;
using SuperMarket.Rules.Interfaces;
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

            //var checkout = new Checkout(itemPriceRules);

            //return checkout;

            return itemPriceRules.ToList();
        }
    }
}