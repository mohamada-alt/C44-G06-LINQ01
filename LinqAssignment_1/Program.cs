
using System;
namespace LinqAssignment_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // ================================
            // Transformation Operators
            // ================================

            List<Product> products = new List<Product>
            {
                new Product { ProductName = "Apple", Category = "Fruit", UnitPrice = 1.2m, UnitsInStock = 0 },
                new Product { ProductName = "Banana", Category = "Fruit", UnitPrice = 2.5m, UnitsInStock = 10 },
                new Product { ProductName = "Carrot", Category = "Vegetable", UnitPrice = 4.0m, UnitsInStock = 20 },
                new Product { ProductName = "Desk", Category = "Office", UnitPrice = 200m, UnitsInStock = 5 },
                new Product { ProductName = "Chair", Category = "Office", UnitPrice = 100m, UnitsInStock = 15 }
            };

            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, Total = 450.0m, OrderDate = new DateTime(1998, 5, 12) },
                new Order { OrderId = 2, Total = 800.0m, OrderDate = new DateTime(2000, 7, 21) },
                new Order { OrderId = 3, Total = 200.0m, OrderDate = new DateTime(1997, 3, 5) }
            };

            Console.WriteLine("=== Transformation Operators ===");

            // 1. Names of products
            var productNames = products.Select(p => p.ProductName);
            Console.WriteLine("\n1. Product Names:");
            foreach (var name in productNames) Console.WriteLine(name);

            // 2. Upper and lower case
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var wordCases = words.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            Console.WriteLine("\n2. Word Cases:");
            foreach (var wc in wordCases) Console.WriteLine($"{wc.Upper} / {wc.Lower}");

            // 3. Pairs numbersA < numbersB
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var pairs = from a in numbersA
                        from b in numbersB
                        where a < b
                        select new { A = a, B = b };
            Console.WriteLine("\n3. Pairs A < B:");
            foreach (var p in pairs) Console.WriteLine($"{p.A}, {p.B}");

            // 4. Orders < 500
            var smallOrders = orders.Where(o => o.Total < 500);
            Console.WriteLine("\n4. Orders < 500:");
            foreach (var o in smallOrders) Console.WriteLine($"Order {o.OrderId}, Total = {o.Total}");

            // 5. Value matches index
            int[] arr1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var indexMatch = arr1.Select((num, index) => new { Number = num, IsMatch = (num == index) });
            Console.WriteLine("\n5. Index Match:");
            foreach (var im in indexMatch) Console.WriteLine($"{im.Number}: {im.IsMatch}");

            // ================================
            // Restriction Operators
            // ================================
            Console.WriteLine("\n=== Restriction Operators ===");

            // 1. Out of stock
            var outOfStock = products.Where(p => p.UnitsInStock == 0);
            Console.WriteLine("\n1. Out of Stock:");
            foreach (var p in outOfStock) Console.WriteLine(p.ProductName);

            // 2. In stock and > 3.00
            var expensiveInStock = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.0m);
            Console.WriteLine("\n2. In Stock & > 3.00:");
            foreach (var p in expensiveInStock) Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

            // 3. Value == Index
            int[] arr2 = { 3, 4, 2, 0, 1, 5, 6 };
            var matches = arr2.Select((num, index) => new { Number = num, Index = index })
                              .Where(x => x.Number == x.Index);
            Console.WriteLine("\n3. Value == Index:");
            foreach (var m in matches) Console.WriteLine($"{m.Number} @ {m.Index}");

            // 4. Some properties
            var productSummary = products.Select(p => new { p.ProductName, Price = p.UnitPrice, p.Category });
            Console.WriteLine("\n4. Product Summary:");
            foreach (var ps in productSummary) Console.WriteLine($"{ps.ProductName} ({ps.Category}) - {ps.Price}");

            // 5. Orders in 1998+
            var recentOrders = orders.Where(o => o.OrderDate.Year >= 1998);
            Console.WriteLine("\n5. Orders 1998+:");
            foreach (var ro in recentOrders) Console.WriteLine($"Order {ro.OrderId}, Date = {ro.OrderDate.Year}");

            // ================================
            // Ordering Operators
            // ================================
            Console.WriteLine("\n=== Ordering Operators ===");

            // 1. By name
            var sortedByName = products.OrderBy(p => p.ProductName);
            Console.WriteLine("\n1. Products by Name:");
            foreach (var p in sortedByName) Console.WriteLine(p.ProductName);

            // 2. By stock descending
            var sortedByStock = products.OrderByDescending(p => p.UnitsInStock);
            Console.WriteLine("\n2. Products by Stock Desc:");
            foreach (var p in sortedByStock) Console.WriteLine($"{p.ProductName}: {p.UnitsInStock}");

            // 3. Digits by length then alpha
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = digits.OrderBy(d => d.Length).ThenBy(d => d);
            Console.WriteLine("\n3. Digits sorted:");
            foreach (var d in sortedDigits) Console.WriteLine(d);

            // 4. Products by category then price desc
            var sortedByCategoryPrice = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            Console.WriteLine("\n4. Products by Category then Price:");
            foreach (var p in sortedByCategoryPrice) Console.WriteLine($"{p.Category} - {p.ProductName} - {p.UnitPrice}");

            // 5. Words by length then case-insensitive desc
            string[] words2 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var sortedWords = words2.OrderBy(w => w.Length)
                                    .ThenByDescending(w => w, StringComparer.OrdinalIgnoreCase);
            Console.WriteLine("\n5. Words sorted:");
            foreach (var w in sortedWords) Console.WriteLine(w);

            // 6. Digits with 2nd letter = i reversed
            var secondLetterI = digits.Where(d => d.Length > 1 && d[1] == 'i').Reverse();
            Console.WriteLine("\n6. Digits with 2nd letter = 'i' (Reversed):");
            foreach (var d in secondLetterI) Console.WriteLine(d);

            Console.WriteLine("\n=== END ===");
        }
    }

    // Supporting Classes
    public class Product
    {
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
    }
}