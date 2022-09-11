using JsonSerializeDeserialize.Config;
using JsonSerializeDeserialize.Domain;
using System.Globalization;

namespace JsonSerializeDeserialize.Services;

internal static class DataService
{
    public static List<Item> GetSeedData(int numRows)
    {
        if (numRows <= 0)
        {
            throw new InvalidOperationException($"Value must be positive integer. Parameter name: {nameof(numRows)}");
        }

        var list = new List<Item>();
        var rnd = new Random();
        for (int i = 1; i <= numRows; i++)
        {
            var category = rnd.Next(1, 4);
            var (Min, Max) = GetPriceRange(category);
            list.Add(
                new Item
                {
                    Name = $"Item {i}",
                    Category = $"Category {category}",
                    Price = GetPrice(rnd.NextDouble(), Min, Max)
                });
        }

        return list;
    }

    public static bool CompareLists(List<Item> list1, List<Item> list2)
    {
        if (list1 == null)
        {
            throw new ArgumentNullException(nameof(list1));
        }

        return list2 == null ? throw new ArgumentNullException(nameof(list1)) : list1.SequenceEqual(list2);
    }

    public static void PerformAnalysis(List<Item> list)
    {
        if (list is null)
        {
            return;
        }

        var columnLayout = "|{0,11}|{1,11}|{2,13}|{3,14}|{4,14}|";
        var dottedLine = string.Format(
            columnLayout,
            new String('-', 11),
            new String('-', 11),
            new String('-', 13),
            new String('-', 14),
            new String('-', 14));
        var headerLine = string.Format(
            columnLayout,
            "Category",
            "Item Count",
            "Lowest Price",
            "Highest Price",
            "Average Price");
        ConsoleEx.WriteLine(dottedLine, 2);
        ConsoleEx.WriteLine(headerLine, 2);
        ConsoleEx.WriteLine(dottedLine, 2);
        var results = list
            .GroupBy(item => item.Category)
            .Select(group =>
                new
                {
                    Metric = group.Key,
                    Count = group.Count(),
                    Min = ConvertToCurrency(group.Min(i => ConvertToDecimal(i.Price))),
                    Max = ConvertToCurrency(group.Max(i => ConvertToDecimal(i.Price))),
                    Avg = ConvertToCurrency(group.Average(i => ConvertToDecimal(i.Price))),
                })
            .OrderBy(x => x.Metric)
            .ToList();
        results.Add(
            new
            {
                Metric = "Overall",
                list.Count,
                Min = ConvertToCurrency(list.Min(i => ConvertToDecimal(i.Price))),
                Max = ConvertToCurrency(list.Max(i => ConvertToDecimal(i.Price))),
                Avg = ConvertToCurrency(list.Average(i => ConvertToDecimal(i.Price))),
            });
        results.ForEach(r =>
            ConsoleEx.WriteLine(
                string.Format(
                    columnLayout,
                    r.Metric,
                    r.Count,
                    r.Min,
                    r.Max,
                    r.Avg), 2));
        ConsoleEx.WriteLine(dottedLine, 2);
    }

    private static (int Min, int Max) GetPriceRange(int category)
    {
        int min;
        int max;
        switch (category)
        {
            case 2:
                min = 100;
                max = 200;
                break;
            case 3:
                min = 200;
                max = 300;
                break;
            default:
                min = 1;
                max = 100;
                break;
        }

        return (min, max);
    }
    private static string GetPrice(double random, int min, int max)
    {
        var price = (random * (max - min)) + min;
        var priceRounded = (decimal)Math.Round(price, 2);
        return $"${string.Format("{0:0.00}", priceRounded)}";
    }

    private static string ConvertToCurrency(decimal price)
    {
        var priceRounded = Math.Round(price, 2);
        return $"${string.Format("{0:0.00}", priceRounded)}";
    }

    private static decimal ConvertToDecimal(string price)
    {
        return decimal.TryParse(price, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out decimal value)
            ? value
            : throw new InvalidOperationException($"Value cannot be converted to decimal. Parameter name: {nameof(price)}");
    }
}
