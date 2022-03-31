using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models
{
    public class ConsoleManager
    {
        Manager m;
        public ConsoleManager(Manager m)
        {
            this.m = m;
        }
        public void Init()
        {
            Console.WriteLine("Welcome to the best shoe store in the world ---Kingdom shoes of Shimi--- !!!");
            Console.WriteLine("Are you manager or a customer?");
            Console.WriteLine("Press 1 to manager, press 2 to customer.");
            bool a = int.TryParse(Console.ReadLine(), out int num);
            while (!a)
            {
                Console.WriteLine("Please enter only numbers.");
                a = int.TryParse(Console.ReadLine(), out num);
            }
            while (a)
            {
                while (num != 1 || num != 2)
                {
                    switch (num)
                    {
                        case 1:
                            PasswordManager();
                            break;
                        case 2:
                            OptionsToCustomer();
                            break;
                        default:
                            Console.WriteLine("Please enter only 1 or 2 !!!");
                            a = int.TryParse(Console.ReadLine(), out num);
                            break;
                    }
                }
            }
        }
        private void PasswordManager()
        {
            Console.WriteLine("Hello Manager, write password please:");
            string a = Console.ReadLine().ToUpper();
            while (a != "shimi111".ToUpper())
            {
                Console.WriteLine("You password an incorrect, Try again !!!");
                a = Console.ReadLine().ToUpper();
            }
            if (a == "shimi111".ToUpper()) OptionsToManager();
        }
        private void OptionsToManager()
        {
            bool countinueSell = true;
            while (countinueSell)
            {
                Console.WriteLine("Manager you have 5 options, choose one:");
                Console.WriteLine("(1.See all distribution point by incidence)");
                Console.WriteLine("(2.Add new distribution point)");
                Console.WriteLine("(3.See all stock)");
                Console.WriteLine("(4.Add new shoe)");
                Console.WriteLine("(5.Exit)");
                bool a = int.TryParse(Console.ReadLine(), out int num);
                while (!a || num != 1 && num != 2 && num != 3 && num != 4 && num != 5)
                {
                    Console.WriteLine("Please enter only numbers (1,2,3,4,5):");
                    a = int.TryParse(Console.ReadLine(), out num);
                }
                switch (num)
                {
                    case 1:
                        m.PrintDistributionPointsByIncidence(Print);
                        break;
                    case 2:
                        AddPoint();
                        break;
                    case 3:
                        m.PrintAll(Print);
                        break;
                    case 4:
                        AddANewShoeManager(m);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                MenuOrExitManager();
            }
        }
        private void AddANewShoeManager(Manager m)
        {
            Console.WriteLine("Please follow these steps:");
            Console.Write("Shoe brand: ");
            string brand = Console.ReadLine().ToUpper();
            Console.WriteLine();
            Console.Write("Shoe model: ");
            string model = Console.ReadLine().ToUpper();
            Console.WriteLine();
            Console.Write("Shoe size: ");
            bool isNum = float.TryParse(Console.ReadLine(), out float size);
            while (!isNum)
            {
                Console.WriteLine("Enter only numbers: ");
                isNum = float.TryParse(Console.ReadLine(), out size);
            }
            Console.WriteLine();
            Console.Write("How many shoes would you like to put in stock: ");
            isNum = int.TryParse(Console.ReadLine(), out int amount);
            while (!isNum)
            {
                Console.WriteLine("enter only numbers: ");
                isNum = int.TryParse(Console.ReadLine(), out amount);
            }
            m.AddShoes(brand, model, size, amount);
            Console.WriteLine($"You added successfully: \n{brand}, {model}, {size}, {amount}");
            Thread.Sleep(2500);
        }
        private void AddPoint()
        {
            Console.WriteLine("What is the name of the distribution point:");
            string NamedistributionPoint = Console.ReadLine();
            int isPostalCode = CustomerCheckClosestPoint();
            m.AddDistributionPoints(NamedistributionPoint, isPostalCode);
            Console.WriteLine($"Good, you've added a new distribution point:{NamedistributionPoint}, {isPostalCode} \n");
            Console.WriteLine();
        }
        private void MenuOrExitManager()
        {
            Console.WriteLine("Press (1) to exit, Press any other key to return to the menu");
            int.TryParse(Console.ReadLine(), out int num);
            if (num == 1) Environment.Exit(0);
        }
        private void OptionsToCustomer()
        {
            Console.WriteLine("Very pleasant!!");
            Console.WriteLine("Here you can find shoes in a variety of newest brands and models on the market.\n");
            Thread.Sleep(2500);
            bool countinueSell = true;
            while (countinueSell)
            {
                Console.WriteLine("Customer you have 4 options, choose one:\n");
                Console.WriteLine("(1.See all stock)");
                Console.WriteLine("(2.Buy shoe)");
                Console.WriteLine("(3.See all distribution point)");
                Console.WriteLine("(4.Exit)");
                Console.WriteLine();
                bool a = int.TryParse(Console.ReadLine(), out int num);
                while (!a || num != 1 && num != 2 && num != 3 && num != 4)
                {
                    Console.WriteLine("Please enter only numbers (1,2,3,4).");
                    a = int.TryParse(Console.ReadLine(), out num);
                }
                switch (num)
                {
                    case 1:
                        m.PrintAll(Print);
                        break;
                    case 2:
                        BuyShoe();
                        break;
                    case 3:
                        m.PrintDistributionPointsByIncidence(Print);
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
                MenuOrExitCustomer();
            }
        }
        private void SelectDistributionPoints()
        {
            Console.WriteLine("Do you want to collect the package from one of our existing distribution points?");
            Console.WriteLine("Otherwise we will give you the package to the distribution point closest to your home.");
            Console.WriteLine();
            m.PrintDistributionPointsByIncidence(Print);
            Console.WriteLine();
            Thread.Sleep(2500);
            int zipCode = CustomerCheckClosestPoint();
            DistributionPiont dp = new DistributionPiont();
            dp = m.FindClossedDistributionPiont(new DistributionPiont("", zipCode));
            dp.Incidence++;
            Console.WriteLine();
            Console.WriteLine("Everything was successful, you can take your order from the selected distribution point.");
            Console.WriteLine(dp);
            Console.WriteLine("Thank you and continue a pleasant day.");
            Console.WriteLine();
        }
        private void BuyShoe()
        {
            Console.WriteLine("Shoe brand:");
            string brand = Console.ReadLine().ToUpper();
            while (!m.CheckShoeBrand(brand))
            {
                Console.WriteLine("The brand not exist, enter brand again:");
                brand = Console.ReadLine().ToUpper();
            }
            Console.WriteLine("Shoe model:");
            string model = Console.ReadLine().ToUpper();
            while (!m.CheckShoeModel(brand, model))
            {
                Console.WriteLine("The model not exist, enter model again:");
                model = Console.ReadLine().ToUpper();
            }
            Console.WriteLine("Shoe size:");
            bool size = float.TryParse(Console.ReadLine(), out float num1);
            while (!size)
            {
                Console.WriteLine("Enter numbers only !!!");
                size = float.TryParse(Console.ReadLine(), out num1);
            }
            Console.WriteLine("Choose amount");
            bool amount = int.TryParse(Console.ReadLine(), out int num2);
            while (!amount)
            {
                Console.WriteLine("Enter numbers only !!!");
                amount = int.TryParse(Console.ReadLine(), out num2);
            }
            SizeAndAmount s = new SizeAndAmount();
            s = m.CheckSizeAndAmount(brand, model, num1, num2);
            if (s == null || s.Amount < num2)
            {
                Console.WriteLine("We dont have size or amount in stock!!");
                return;
            }
            Console.WriteLine();
            m.BuyShoes(brand, model, num1, num2, Print);
            Console.WriteLine("Congratulations, the purchase was made!!");
            Console.WriteLine("We move you to choose a distribution point where you can pick up your shoes.");
            Console.WriteLine();
            Thread.Sleep(2500);
            SelectDistributionPoints();
        }
        private int CustomerCheckClosestPoint()
        {
            Console.WriteLine("what is the postal code (5 numbers).");
            bool postalCode = int.TryParse(Console.ReadLine(), out int isNum);
            while (!postalCode)
            {
                Console.WriteLine("enter only numbers !!! ");
                postalCode = int.TryParse(Console.ReadLine(), out isNum);
            }
            while (isNum <= 10000 || isNum >= 99999)
            {
                Console.WriteLine("enter only 5 numbers !!! ");
                postalCode = int.TryParse(Console.ReadLine(), out isNum);
                if (isNum >= 10000 && isNum <= 99999)
                    break;
            }
            return isNum;
        }
        private void MenuOrExitCustomer()
        {
            Console.WriteLine("Press (1) to exit, Press any other key to return to the menu");
            int.TryParse(Console.ReadLine(), out int num);
            if (num == 1) Environment.Exit(0);
        }
        private void Print(string a)
        {
            Console.WriteLine(a);
        }
    }
}

