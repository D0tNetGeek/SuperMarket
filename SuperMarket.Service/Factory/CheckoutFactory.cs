namespace SuperMarket.Service.Factory
{
    public class CheckoutFactory
    {
        private readonly ItemPriceRuleFactory _itemPriceRuleFactory;

        public CheckoutFactory(ItemPriceRuleFactory itemPriceRuleFactory)
        {
            _itemPriceRuleFactory = itemPriceRuleFactory;
        }

        public ICheckout CreateCheckout()
        {
            var itemPriceRules = _itemPriceRuleFactory.GetAllItemPriceRules();

            var checkout = new Checkout(itemPriceRules);

            return checkout;
        }
    }
}