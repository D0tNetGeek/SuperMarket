using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperMarket.Repository;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Rules.ItemPriceRules;
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
            var product = new Product {Sku = "A99", UnitPrice = .50m};
            var expectedPrice = .50m;

            //Act
            _checkoutFactory.Setup(x => x.CreateCheckout()).Returns(new List<IItemPriceRule>
            {
                new SingleItemPriceRule(product.Sku, product.UnitPrice)
            });

            _repo.Setup(x => x.DisplayAvailableItems()).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Sku = "A99",
                    UnitPrice = 0.50m
                }
            });


            _checkout = new Service.Checkout(_checkoutFactory.Object, _repo.Object);

            _checkout.ScanItem(product.Sku);

            var actualPrice = _checkout.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanMultipleThreeAItemsReturnsTotalPrice()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product{ Sku = "A99", UnitPrice = .50m },
                new Product{ Sku = "A99", UnitPrice = .50m },
                new Product{ Sku = "A99", UnitPrice = .50m }
            };

            var expectedPrice = 1.30m;

            //Act
            _checkoutFactory.Setup(x => x.CreateCheckout()).Returns(new List<IItemPriceRule>
            {
                new MultipleItemPriceRule("A99", 1.30m, 3)
            });

            _repo.Setup(x => x.DisplayAvailableItems()).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Sku = "A99",
                    UnitPrice = 0.50m
                }
            });


            _checkout = new Service.Checkout(_checkoutFactory.Object, _repo.Object);

            foreach (var item in products)
            {
                _checkout.ScanItem(item.Sku);
            }
            

            var actualPrice = _checkout.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanMutlipleTwoBItemsReturnsTotalPrice()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product{ Sku = "B15", UnitPrice = .30m },
                new Product{ Sku = "B15", UnitPrice = .30m }
            };

            var expectedPrice = 0.45m;

            //Act
            _checkoutFactory.Setup(x => x.CreateCheckout()).Returns(new List<IItemPriceRule>
            {
                new MultipleItemPriceRule("B15", .45m, 2)
            });

            _repo.Setup(x => x.DisplayAvailableItems()).Returns(new List<ProductDto>
            {
                new ProductDto
                {
                    Sku = "B15",
                    UnitPrice = 0.30m
                }
            });


            _checkout = new Service.Checkout(_checkoutFactory.Object, _repo.Object);

            foreach (var item in products)
            {
                _checkout.ScanItem(item.Sku);
            }


            var actualPrice = _checkout.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedPrice, actualPrice);
        }

        [TestMethod]
        public void ScanThreeAandTwoBItemsReturnsTotalPrice()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product{ Sku = "A99", UnitPrice = .50m },
                new Product{ Sku = "A99", UnitPrice = .50m },
                new Product{ Sku = "A99", UnitPrice = .50m },
                new Product{ Sku = "B15", UnitPrice = .30m },
                new Product{ Sku = "B15", UnitPrice = .30m }
            };

            var expectedPrice = 1.75m;

            //Act
            _checkoutFactory.Setup(x => x.CreateCheckout()).Returns(new List<IItemPriceRule>
            {
                new MultipleItemPriceRule("B15", .45m, 2)
            });

            _repo.Setup(x => x.DisplayAvailableItems()).Returns(new List<ProductDto>
            {
                new ProductDto{ Sku = "A99", UnitPrice = .50m },
                new ProductDto{ Sku = "A99", UnitPrice = .50m },
                new ProductDto{ Sku = "A99", UnitPrice = .50m },
                new ProductDto{ Sku = "B15", UnitPrice = .30m },
                new ProductDto{ Sku = "B15", UnitPrice = .30m }
            });


            _checkout = new Service.Checkout(_checkoutFactory.Object, _repo.Object);

            foreach (var item in products)
            {
                _checkout.ScanItem(item.Sku);
            }


            var actualPrice = _checkout.CalculateTotalPrice();

            //Assert
            Assert.AreEqual(expectedPrice, actualPrice);
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
