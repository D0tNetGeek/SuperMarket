using System.Collections.Generic;

namespace SuperMarket.Repository
{
    public class SuperMarketData : ISuperMarketData
    {
        public List<ProductDto> DisplayAvailableItems()
        {
            List<ProductDto> availableItems = new List<ProductDto>
            {
                new ProductDto{
                    Sku = "A99",
                    Description = "Apple",
                    UnitPrice = 0.50m
                },
                new ProductDto
                {
                    Sku = "B15",
                    Description = "Biscuit",
                    UnitPrice = 0.30m
                }
                ,
                new ProductDto
                {
                    Sku = "C40",
                    Description = "Coffee",
                    UnitPrice = 1.80m
                }
                ,
                new ProductDto
                {
                    Sku = "T23",
                    Description = "Tissues",
                    UnitPrice = 0.99m
                }
            };

            return availableItems;
        }
    }
}
