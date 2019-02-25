using System.Collections.Generic;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;

namespace SuperMarket.Service.Interfaces
{
    public interface ICheckoutFactory
    {
        //ICheckout CreateCheckout();

        List<IItemPriceRule> CreateCheckout();
        List<Product> GetAvailableItems();
    }
}
