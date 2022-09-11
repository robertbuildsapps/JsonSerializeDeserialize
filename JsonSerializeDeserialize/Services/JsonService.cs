using JsonSerializeDeserialize.Config;
using JsonSerializeDeserialize.Domain;
using System.Text;
using System.Text.Json;

namespace JsonSerializeDeserialize.Services;

internal static class JsonService
{
    public static string SerializeJson(List<Item> list, string fileName, string rootName)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        if (string.IsNullOrWhiteSpace(rootName))
        {
            throw new ArgumentNullException(nameof(rootName));
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var json = JsonSerializer.Serialize(list, options);
        var sb = new StringBuilder()
                    .Append(SpecialChars.OpenCurlyBracket)
                    .Append(SpecialChars.DoubleQuotes)
                    .Append(rootName)
                    .Append(SpecialChars.DoubleQuotes)
                    .Append(SpecialChars.Colon)
                    .Append(json)
                    .Append(SpecialChars.CloseCurlyBracket);
        var fileContent = sb.ToString();
        var filePath = FileService.WriteFile(fileName, fileContent);
        return filePath;
    }

    public static string SerializeCsv(List<Item> list, string fileName)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        var items = list.Select(i => string.Join(SpecialChars.Comma, i.Name, i.Category, i.Price));
        var fileContent = string.Join(Environment.NewLine, items);
        fileContent = "Name,Category,Price" + "\n" + fileContent;
        var filePath = FileService.WriteFile(fileName, fileContent);
        return filePath;
    }

    public static List<Item> DeserializeJson(string filePath, string rootName)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (string.IsNullOrWhiteSpace(rootName))
        {
            throw new ArgumentNullException(nameof(rootName));
        }

        var json = FileService.ReadFile(filePath);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var elements = root.GetProperty(rootName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var list = JsonSerializer.Deserialize<List<Item>>(elements, options);
        return list ?? new List<Item>();
    }

    public static List<Item> DeserializeJsonByEnumeration(string filePath, string rootName)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (string.IsNullOrWhiteSpace(rootName))
        {
            throw new ArgumentNullException(nameof(rootName));
        }

        var json = FileService.ReadFile(filePath);
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var elements = root.GetProperty(rootName);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var list = new List<Item>();

        foreach (var element in elements.EnumerateArray())
        {
            var item = JsonSerializer.Deserialize<Item>(element, options);

            if (item != null)
            {
                list.Add(item);
            }
        }

        return list;
    }

    public static List<Item> DeserializeCsv(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        var csv = FileService.ReadLines(filePath);
        List<Item> list =
            csv
            .Skip(1)
            .Select(v => DeserializeCsvLine(v))
            .ToList();
        return list;
    }

    private static Item DeserializeCsvLine(string line)
    {
        string[] values = line.Split(SpecialChars.Comma);
        return new Item()
        {
            Name = values[0],
            Category = values[1],
            Price = values[2]
        };
    }
}
