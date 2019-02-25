using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Repository;
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
                .AddSingleton<ISuperMarketData, SuperMarketData>()
                .BuildServiceProvider();

            var checkoutService = serviceProvider.GetService<ICheckout>();

            int userInput = 0;
            bool isValid = true;

            do
            {
                isValid = true;

                userInput = DisplayMenu();

                switch (userInput)
                {
                    case 1:
                        //Display available items.

                        System.Console.Clear();

                        var availableItems = checkoutService.DisplayAvailableItems();

                        System.Console.WriteLine("\t\t\t---------------------------------------------------");
                        System.Console.WriteLine("\t\t\t                Available Items                  ");
                        System.Console.WriteLine("\t\t\t---------------------------------------------------");
                        System.Console.WriteLine();
                        System.Console.WriteLine("\t\t\tProductId" + "\t" + "SKU" + "\t" + "Description" + "\t" + "Unit Price");
                        System.Console.WriteLine("\t\t\t--------------------------------------------------");

                        int i = 0;
                        foreach (var item in availableItems)
                        {
                            i++;
                            System.Console.WriteLine("\t\t\t" + i+ "\t\t" + item.Sku + "\t" + item.Description + "\t\t" + item.UnitPrice);
                        }

                        System.Console.WriteLine();

                        break;

                    case 2:
                        //Accept an item.

                        System.Console.Write("\t\t\tEnter the sku you want to purchase: ");

                        var sku = System.Console.ReadLine();

                        checkoutService.ScanItem(sku);

                        break;

                    case 3:
                        //Checkout basket and return total price.

                        System.Console.Clear();

                        var totalPrice = checkoutService.CalculateTotalPrice();

                        var basketItems = checkoutService.BasketItems().Count > 0
                            ? string.Join(", ", checkoutService.BasketItems())
                            : "No items purchased";

                        System.Console.WriteLine("\t\t\tItems in the basket : " + basketItems);

                        System.Console.WriteLine("\t\t\tThe Total Price for the Basket is: " + totalPrice);

                        break;

                    default:
                        isValid = false;
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

            System.Console.WriteLine();
            System.Console.Write("{0," + ((System.Console.WindowWidth / 2) + (("Enter your menu choice: ").Length / 2)) + "}", "Enter your menu choice: ");

            var result = Int32.TryParse(System.Console.ReadLine(), out var input);

            if(!result || input > 4)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("{0," + ((System.Console.WindowWidth / 2) + (("You have entered an invalid menu option, press any key to continue again...").Length / 2)) + "}", "You have entered an invalid menu option, press any key to continue again...");
                System.Console.ReadKey();
                System.Console.Clear();
            }

            return input;
        }
    }
}
