using System.Collections.Generic;
using SuperMarket.Rules.Interfaces;

namespace SuperMarket.Service.Interfaces
{
    public interface ICheckoutFactory
    {
        //ICheckout CreateCheckout();

        List<IItemPriceRule> CreateCheckout();
    }
}
