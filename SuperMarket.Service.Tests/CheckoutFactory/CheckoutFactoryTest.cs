using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperMarket.Repository;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service.Tests.CheckoutFactory
{
    [TestClass]
    public class CheckoutFactoryTest
    {
        private ICheckoutFactory _factory;
        private Mock<IItemPriceRuleFactory> _itemPriceRuleFactory;
        private Mock<ISuperMarketData> _repo;
        private ICheckoutFactory _service;
        private List<ProductDto> _products;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Mock<ISuperMarketData>();
            _itemPriceRuleFactory = new Mock<IItemPriceRuleFactory>();
            //_factory = new Factory.CheckoutFactory(_itemPriceRuleFactory.Object, _repo.Object);

            _service = new Factory.CheckoutFactory(_itemPriceRuleFactory.Object, _repo.Object);

            _products = new List<ProductDto>
            {
                new ProductDto{Sku = "A99", UnitPrice = 0.50m},
                new ProductDto{Sku = "B15", UnitPrice = 0.30m},
                new ProductDto{Sku = "C40", UnitPrice = 1.80m},
                new ProductDto{Sku = "T23", UnitPrice = 0.99m},
            };
        }

        [TestMethod]
        public void ItGetsAllAvailableItems()
        {
            _repo.Setup(r => r.DisplayAvailableItems()).Returns(_products);

            var results = _service.GetAvailableItems();

            Assert.IsNotNull(results);
            Assert.AreEqual(_products.Count, results.Count);
            Assert.IsInstanceOfType(results, typeof(List<Product>));
        }

        [TestMethod]
        public void ItCreatesCheckoutAndGetsAllRules()
        {
            _itemPriceRuleFactory.Setup(x=> x.GetAllItemPriceRules()).Returns(Mock.Of<List<IItemPriceRule>>);

            var results = _factory.CreateCheckout();

            Assert.IsNotNull(results);
        }
    }
}
