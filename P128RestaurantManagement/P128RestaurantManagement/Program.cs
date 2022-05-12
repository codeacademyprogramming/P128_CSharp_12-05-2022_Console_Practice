using System;
using System.Linq;
using P128RestaurantManagement.Enums;
using P128RestaurantManagement.Models;
using P128RestaurantManagement.Services;

namespace P128RestaurantManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantManager restaurantManager = new RestaurantManager();

            do
            {
                Console.WriteLine("1. Menu Uzerinde Emeliyyatlar");
                Console.WriteLine("2. Sifaris Uzerinde Emeliyyatlar");
                Console.WriteLine("3. Sistemden Cixis");

                string choose = Console.ReadLine();
                int chooseNum;

                while (!int.TryParse(choose, out chooseNum) || chooseNum < 1 || chooseNum > 3)
                {
                    Console.WriteLine("Duzgun Secimm Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        MenuOperations(ref restaurantManager);
                        break;
                    case 2:
                        OrderOperations(ref restaurantManager);
                        break;
                    case 3:
                        return;
                }
            } while (true);
        }
        #region Menu Item Operations
        static void MenuOperations(ref RestaurantManager restaurantManager)
        {
            do
            {
                Console.WriteLine("1. Yeni Menu Item Elave Et");
                Console.WriteLine("2. Menu Item Uzerinde Duzelis Et");
                Console.WriteLine("3. Menu Item Sil");
                Console.WriteLine("4. Butun Menu Itemlari Gosder");
                Console.WriteLine("5. Novune Gore Butun Menu Itemlari Gosder");
                Console.WriteLine("6. Qiymet Araligina Gore Butun Menu Itemlari Gosder");
                Console.WriteLine("7. Axdaris");
                Console.WriteLine("8. Sistemden Cixis");

                string choose = Console.ReadLine();
                int chooseNum;

                while (!int.TryParse(choose, out chooseNum) || chooseNum < 1 || chooseNum > 8)
                {
                    Console.WriteLine("Duzgun Secimm Edin");
                    choose = Console.ReadLine();
                }

                switch (chooseNum)
                {
                    case 1:
                        AddMenuItem(ref restaurantManager);
                        break;
                    case 2:
                        EditMenuITem(ref restaurantManager);
                        break;
                    case 3:
                        RemoveMenuItem(ref restaurantManager);
                        break;
                    case 4:
                        ShowAllMenuItem(ref restaurantManager);
                        break;
                    case 5:
                        GetMenuItemByCategroy(ref restaurantManager);
                        break;
                    case 6:
                        GetMenuItemByPriceInterval(ref restaurantManager);
                        break;
                    case 7:
                        SearchMenuItem(ref restaurantManager);
                        break;
                    case 8:
                        return;
                }
            } while (true);
        }

        static void AddMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Menu Item Adini Daxil Et");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || restaurantManager.MenuItems.Any(m=>m.Name == name.Trim().ToUpper()))
            {
                Console.WriteLine("Dzugun Ad Daxil Et");
                name = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Qiymetini Daxil Et");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price < 0)
            {
                Console.WriteLine("Duzgun Qiymet Daxil Et");
                priceStr = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            restaurantManager.AddMenuItem(name.Trim().ToUpper(), price, (Category)categoryNum);
        }

        static void EditMenuITem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Duzelis Etmek Isdediyniz Menu Item-i Siyahidan Secin Nomresini Daxil Edin");
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
            string no = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(no) || !restaurantManager.MenuItems.Any(m=>m.No == no.Trim().ToUpper()) )
            {
                Console.WriteLine("Duzgun Nomre Sec");
                no = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Adini Daxil Et");
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || restaurantManager.MenuItems.Any(m =>m.No != no && m.Name == name.Trim().ToUpper()))
            {
                Console.WriteLine("Dzugun Ad Daxil Et");
                name = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Qiymetini Daxil Et");
            string priceStr = Console.ReadLine();
            double price;

            while (!double.TryParse(priceStr, out price) || price < 0)
            {
                Console.WriteLine("Duzgun Qiymet Daxil Et");
                priceStr = Console.ReadLine();
            }

            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            restaurantManager.EditMenuITem(no, name, price, (Category)categoryNum);
        }

        static void RemoveMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Duzelis Etmek Isdediyniz Menu Item-i Siyahidan Secin Nomresini Daxil Edin");
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
            string no = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(no) || !restaurantManager.MenuItems.Any(m => m.No == no.Trim().ToUpper()))
            {
                Console.WriteLine("Duzgun Nomre Sec");
                no = Console.ReadLine();
            }

            restaurantManager.RemoveMenuItem(no);
        }

        static void ShowAllMenuItem(ref RestaurantManager restaurantManager)
        {
            foreach (MenuItem menuItem in restaurantManager.MenuItems)
            {
                Console.WriteLine(menuItem);
            }
        }

        static void GetMenuItemByCategroy(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Menu Item Novunu Sec");
            foreach (var item in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"{(int)item} {item}");
            }
            string categoryStr = Console.ReadLine();
            int categoryNum;

            while (!int.TryParse(categoryStr, out categoryNum) || categoryNum < 1 || categoryNum > 4)
            {
                Console.WriteLine("Duzgun Nov Sec");
                categoryStr = Console.ReadLine();
            }

            foreach (MenuItem item in restaurantManager.GetMenuItemByCategroy((Category)categoryNum))
            {
                Console.WriteLine(item);
            }
        }

        static void GetMenuItemByPriceInterval(ref RestaurantManager restaurantManager)
        {
            step1:
            Console.WriteLine("Baslangic Qiymeti Daxil Et:");
            string minStr = Console.ReadLine();
            double min;

            while (!double.TryParse(minStr, out min) || min < 0)
            {
                Console.WriteLine("Duzgun Balangic Qiymet Daxil Et");
                minStr = Console.ReadLine();
            }

            Console.WriteLine("Son Qiymeti Daxil Et:");
            string maxStr = Console.ReadLine();
            double max;

            while (!double.TryParse(maxStr, out max) || max < 0)
            {
                Console.WriteLine("Duzgun Son Qiymet Daxil Et");
                maxStr = Console.ReadLine();
            }

            if (min > max)
            {
                goto step1;
            }

            foreach (MenuItem item in restaurantManager.GetMenuItemByPriceInterval(min,max))
            {
                Console.WriteLine(item);
            }
        }

        static void SearchMenuItem(ref RestaurantManager restaurantManager)
        {
            Console.WriteLine("Axdaris Deyerini Daxil Et");
            string search = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(search))
            {
                Console.WriteLine("Duzgun Axtaris Deyeri Daxil Et");
                search = Console.ReadLine();
            }

            foreach (MenuItem item in restaurantManager.SearchMenuItem(search))
            {
                Console.WriteLine(item);
            }
        }
        #endregion



        #region Order Operations
        static void OrderOperations(ref RestaurantManager restaurantManager)
        {

        }
        #endregion
    }
}
