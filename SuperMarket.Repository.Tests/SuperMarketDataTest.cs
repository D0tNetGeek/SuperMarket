using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarket.Repository.Tests
{
    [TestClass]
    public class SuperMarketDataTest
    {
        private ISuperMarketData _repo;

        [TestInitialize]
        public void Setup()
        {
            IEnumerable<ProductDto> products = new List<ProductDto>
            {
                new ProductDto{Sku = "A99", UnitPrice = 0.50m},
                new ProductDto{Sku = "B15", UnitPrice = 0.30m},
                new ProductDto{Sku = "C40", UnitPrice = 1.80m},
                new ProductDto{Sku = "T23", UnitPrice = 0.99m},
            };

            _repo = new SuperMarketData();
        }

        [TestMethod]
        public void ItGetsAllAvailableItems()
        {
            //Arrange

            //Act
            var availableItems = _repo.DisplayAvailableItems();

            //Assert
            Assert.IsNotNull(availableItems);
        }
    }
}
