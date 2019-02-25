using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperMarket.Repository;
using SuperMarket.Rules.Factory;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service.Tests.Checkout
{
    [TestClass]
    public class CheckoutTest
    {
        private ICheckout  _checkout;
        private Mock<ICheckoutFactory> _checkoutFactory;
        private Mock<ISuperMarketData> _repo;

        [TestInitialize]
        public void Setup()
        {
            _repo = new Mock<ISuperMarketData>();
            _checkoutFactory = new Mock<ICheckoutFactory>();
            _checkout = new Service.Checkout(_checkoutFactory.Object, _repo.Object);
        }

        [TestMethod]
        public void ItGetsAllAvailableItems()
        {
            //Arrage

            //Act
            _repo.Setup(x => x.DisplayAvailableItems()).Returns(Mock.Of<List<ProductDto>>);

            var results = _checkout.DisplayAvailableItems();

            //Assert
            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<Product>));
        }

        [TestMethod]
        public void ScanSingleItemAndReturnTotalPrice()
        {
            //Arrange
            var product = new Product {Sku = "A99"};
            var expectedPrice = .50m;

            //Act
            _checkout.BasketItems();
            _checkoutFactory.Setup(x => x.CreateCheckout()).Returns(Mock.Of<List<IItemPriceRule>>);
            _repo.Setup(x => x.DisplayAvailableItems()).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Sku = "A99",
                    UnitPrice = 0.50m
                }
            });

            _checkout.ScanItem(product.Sku);

            var actualPrice = _checkout.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanMultipleThreeAItemsReturnsTotalPrice()
        {
            var expectedPrice = 1.30m;
            var checkout = CreateCheckout();

            //checkout.Scan("A99");
            //checkout.Scan("A99");
            //checkout.Scan("A99");

            //var actualPrice = checkout.CalculateTotalPrice();

            //Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanMutlipleTwoBItemsReturnsTotalPrice()
        {
            var expectedPrice = 4.5m;
            var checkout = CreateCheckout();

            //checkout.Scan("B15");
            //checkout.Scan("B15");

            //var actualPrice = checkout.CalculateTotalPrice();

            //Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanThreeAandTwoBItemsReturnsTotalPrice()
        {
            var expectedPrice = 1.75m;
            var checkout = CreateCheckout();

            //checkout.Scan("A99");
            //checkout.Scan("A99");
            //checkout.Scan("A99");
            //checkout.Scan("B15");
            //checkout.Scan("B15");

            //var actualPrice = checkout.CalculateTotalPrice();

            //Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanThreeAAndTwoBItemsNotInOrderReturnsTotalPrice()
        {
            var expectedPrice = 1.75m;
            var checkout = CreateCheckout();

            //checkout.Scan("A99");
            //checkout.Scan("B15");
            //checkout.Scan("A99");
            //checkout.Scan("B15");
            //checkout.Scan("A99");

            //var actualPrice = checkout.CalculateTotalPrice();

            //Assert.AreEqual(expectedPrice, actualPrice);
        }

        private static List<IItemPriceRule> CreateCheckout()
        {
            var itemrule = new Mock<IItemPriceRuleFactory>();
            var repo = new Mock<ISuperMarketData>();

            return new Factory.CheckoutFactory(itemrule.Object, repo.Object).CreateCheckout();
        }
    }
}
