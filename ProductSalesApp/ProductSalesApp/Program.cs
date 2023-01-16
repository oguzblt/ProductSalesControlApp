using ProductSalesApp.EntityFramework;
using ProductSalesApp.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSalesApp
{
    internal class Program
    {
        public static Context db = new Context();
        public static Category category = new Category();
        public static Product product = new Product();
        public static Sales sales = new Sales();
        static void Main(string[] args)
        {
            while (true)
            {
X:
                Console.WriteLine("Product Sales App\n\n");
                Console.WriteLine("Main Menu");
                Console.WriteLine("----------------- \n");
                Console.WriteLine("Category          [1]");
                Console.WriteLine("Product           [2]");
                Console.WriteLine("Sales             [3]\n");
                Console.WriteLine("Exit              [0]\n");
                Console.Write("Choice: ");
                int choice = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        CategoryProductMenu(choice);
                        goto X;
                    case 2:
                        CategoryProductMenu(choice);
                        goto X;
                    case 3:
                        SalesMenu();
                        goto X;
                    case 0:
                        Environment.Exit(choice);
                        break;
                }
            }
        }
        // Category and Product Menu
        public static void CategoryProductMenu(int choice)
        {
            Console.WriteLine("List      [1]");
            Console.WriteLine("Add       [2]");
            Console.WriteLine("Remove    [3]");
            Console.WriteLine("Update    [4]\n");
            Console.Write("Choice: ");
            int crudchoice = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (crudchoice)
            {
                case 1:
                    if (choice == 1)
                    {
                        CategoryList();  
                    }
                    else if (choice == 2)
                    {
                        ProductList();
                    }
                    break;
                case 2:
                    if (choice == 1)
                    {
                        Console.Write("Category Name: ");
                        string catAd = Console.ReadLine();
                        Console.WriteLine(CategoryAdd(catAd));
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Product Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Product Stock: ");
                        int stok = int.Parse(Console.ReadLine());
                        Console.Write("Product Price: ");
                        int price = int.Parse(Console.ReadLine());
                        Console.Write("Category Id: ");
                        int catid = int.Parse(Console.ReadLine());
                        Console.WriteLine(ProductAdd(name, stok, catid, price));
                    }
                    break;
                case 3:
                    if (choice == 1)
                    {
                        Console.WriteLine("Category Id: ");
                        int catId = int.Parse(Console.ReadLine());
                        Console.WriteLine(CategoryRemove(catId));
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Product Id: ");
                        int proId = int.Parse(Console.ReadLine());
                        Console.WriteLine(ProductRemove(proId));
                    }
                    break;
                case 4:
                    if (choice == 1)
                    {
                        Console.WriteLine("Category Id: ");
                        int catId = int.Parse(Console.ReadLine());
                        Console.WriteLine("New Category Name: ");
                        string catAd = Console.ReadLine();
                        Console.WriteLine(CategoryUpdate(catId, catAd));
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Product Id: ");
                        int proId = int.Parse(Console.ReadLine());
                        Console.Write("Product Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Product Stock: ");
                        int stok = int.Parse(Console.ReadLine());
                        Console.Write("Product Price: ");
                        int price = int.Parse(Console.ReadLine());
                        Console.Write("Category Id: ");
                        int catid = int.Parse(Console.ReadLine());
                        Console.WriteLine(ProductUpdate(proId, name, stok, catid, price));

                    }
                    break;
            }
        }
        // Category CRUD
        public static void CategoryList()
        {
            foreach (var item in db.Categories)
            {
                Console.WriteLine(item.Id+"."+" "+item.CategoryName);
            };
        }
        public static string CategoryAdd(string categoryname)
        {
            category.CategoryName = categoryname;
            db.Categories.Add(category);
            db.SaveChanges();
            return "Category added successfully";
        }
        public static string CategoryRemove(int id)
        {
            var del = db.Categories.Find(id);
            db.Categories.Remove(del);
            db.SaveChanges();
            return "Category deleted successfully";
        }
        public static string CategoryUpdate(int id, string categoryname)
        {
            var update = db.Categories.Find(id);
            update.CategoryName = categoryname;
            db.SaveChanges();
            return "Category updated successfully";
        }
        // Product CRUD 
        public static void ProductList()
        {
            var categorylist = db.Categories.ToList();
            foreach (var item in db.Products)
            {
                Console.WriteLine(item.Id+"."+" "+item.ProductName+" "+categorylist.FirstOrDefault(x => x.Id== item.CategoryId).CategoryName+" "+item.ProductStock+" "+item.ProductPrice);
            }
        }
        public static string ProductAdd(string name, int stok, int catId, int price)
        {
            product.ProductName = name;
            product.ProductStock = stok;
            product.ProductPrice = price;
            product.CategoryId = catId;
            db.Products.Add(product);
            db.SaveChanges();
            return "Product added successfully";
        }
        public static string ProductRemove(int id)
        {
            var del = db.Products.Find(id);
            db.Products.Remove(del);
            db.SaveChanges();
            return "Product deleted successfully";
        }
        public static string ProductUpdate(int id, string name, int stok, int catId, int price)
        {
            var update = db.Products.Find(id);
            update.ProductName = name;
            update.ProductStock = stok;
            update.ProductPrice = price;
            update.CategoryId = catId;
            db.SaveChanges();
            return "Product updated successfully";
        }
        // Sales Menu and CRUD
        public static void SalesMenu()
        {
            var productlist = db.Products.ToList();
            Console.WriteLine("List     [1]");
            Console.WriteLine("Add      [2]\n");
            Console.Write("Choice: ");
            int saleschoice = int.Parse(Console.ReadLine());
            switch (saleschoice)
            {
                case 1:
                    foreach (var item in db.Sales)
                    {
                        Console.WriteLine(item.Id+"."+" "+productlist.FirstOrDefault(x => x.Id== item.ProductId).ProductName+" "+item.Amount+" "+item.TotalPrice);
                    }
                    break;
                case 2:
                    Console.Write("to be sold product id: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("to be sold product amount: ");
                    int amount = int.Parse(Console.ReadLine());
                    Console.WriteLine(SalesAdd(id, amount));
                    Console.WriteLine(SalesStockUpdate());
                    break;
            }
        }
        public static string SalesAdd(int productId, int amount)
        {
            int unitprice = 0;
            foreach (var item in db.Products)
            {
                if (productId == item.Id)
                {
                    unitprice = item.ProductPrice;
                }
            }
            sales.Amount = amount;
            sales.TotalPrice = amount * unitprice;
            sales.ProductId = productId;
            db.Sales.Add(sales);
            db.SaveChanges();
            return "the sale took place";
        }
        public static string SalesStockUpdate()
        {
            int lastProductId = 0;
            int lastProduct = 0;
            int lastProductStock = 0;
            foreach (var item in db.Sales)
            {
                if (lastProductStock < item.Id)
                {
                    lastProduct = item.Id;
                    lastProductStock = item.Amount;
                    lastProductId = item.ProductId;
                }
            }
            var product = db.Products.Find(lastProductId);
            product.ProductStock =(product.ProductStock - lastProductStock);
            db.SaveChanges();
            return "Stock Updated";
        }
    }
}