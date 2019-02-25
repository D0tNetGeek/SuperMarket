using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperMarket.Service.Entities;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Service.Tests.Checkout
{
    [TestClass]
    public class CheckoutTest
    {
        private ICheckout  _checkout;
        private Mock<ICheckoutFactory> _checkoutFactory;

        [TestInitialize]
        public void Setup()
        {
            _checkoutFactory = new Mock<ICheckoutFactory>();
            _checkout = new Service.Checkout(_checkoutFactory.Object);
        }

        [TestMethod]
        public void ItGetsAllAvailableItems()
        {
            _checkoutFactory.Setup(x => x.GetAvailableItems()).Returns(new List<Product>());

            var results = _checkout.DisplayAvailableItems();

            Assert.IsInstanceOfType(typeof(List<Product>), results.GetType());
        }
    }
}
