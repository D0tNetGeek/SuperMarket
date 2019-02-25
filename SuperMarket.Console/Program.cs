using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Rules.Factory;
using SuperMarket.Rules.Interfaces;
using SuperMarket.Service;
using SuperMarket.Service.Factory;
using SuperMarket.Service.Interfaces;

namespace SuperMarket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICheckout, Checkout>()
                .AddSingleton<ICheckoutFactory,CheckoutFactory>()
                .AddSingleton<IItemPriceRuleFactory, ItemPriceRuleFactory>()
                .BuildServiceProvider();

            var checkoutService = serviceProvider.GetService<ICheckout>();
            //var checkoutFactory = serviceProvider.GetService<ICheckoutFactory>();

            int userInput = 0;

            do
            {
                userInput = DisplayMenu();

                switch (userInput)
                {
                    case 1:
                        //Display available items.
                        var availableItems = checkoutService.DisplayAvailableItems();

                        System.Console.WriteLine("ProductId" + "\t" + "SKU" + "\t" + "Description" + "\t" + "Unit Price");
                        System.Console.WriteLine("-------------------------------------------------");

                        int i = 0;
                        foreach (var item in availableItems)
                        {
                            i++;
                            System.Console.WriteLine(i+ "\t\t" + item.Sku + "\t" + item.Description + "\t\t" + item.UnitPrice);
                        }
                        break;

                    case 2:
                        //Accept an item.
                        System.Console.WriteLine("Enter the sku you want to purchase");

                        var sku = System.Console.ReadLine();

                        checkoutService.ScanItem(sku);

                        break;

                    case 3:
                        //Checkout basket and return total price.
                        var totalPrice = checkoutService.CalculateTotalPrice();

                        System.Console.WriteLine("Items in the basket : " + string.Join(", ", checkoutService.BasketItems()));

                        System.Console.WriteLine("The Total Price for the Basket is: "+ totalPrice);

                        break;

                }
            } while (userInput != 4);
        }

        public static int DisplayMenu()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("SuperMarket Checkout Application").Length / 2)) + "}", "SuperMarket Checkout Application");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("--------------------------------------------").Length / 2)) + "}", "------------------------------------------");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("1. Display Available Items                  ").Length / 2)) + "}", "1. Display Available Items                ");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("2. Enter to the SKU you want to buy         ").Length / 2)) + "}", "2. Accept an item and return correct price");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("3. Checkout basket and return total price   ").Length / 2)) + "}", "3. Checkout basket and return total price ");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("4. Exit                                     ").Length / 2)) + "}", "4. Exit                                   ");
            System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("--------------------------------------------").Length / 2)) + "}", "------------------------------------------");

            var result = System.Console.ReadLine();

            if (string.IsNullOrEmpty(result) || Convert.ToInt32(result) > 4)
            {
                System.Console.WriteLine("You have entered an invalid menu option, press any key to continue again...");
                System.Console.Clear();
                DisplayMenu();
            }

            return Convert.ToInt32(result);
        }
    }
}
