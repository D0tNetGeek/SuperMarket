using System.Collections.Generic;
using SuperMarket.Service.Entities;

namespace SuperMarket.Service.Interfaces
{
    public interface ICheckout
    {
        List<Product> DisplayAvailableItems();
        void ScanItem(string item);
        decimal CalculateTotalPrice();

        List<string> BasketItems();
    }
}