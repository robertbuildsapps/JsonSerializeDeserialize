using System.Reflection;

namespace JsonSerializeDeserialize.Services;

internal static class FileService
{
    public static string ReadFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        return File.Exists(filePath)
            ? File.ReadAllText(filePath)
            : throw new InvalidOperationException($"File does not exist. Parameter name: {nameof(filePath)}");
    }

    public static string[] ReadLines(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        return File.Exists(filePath)
            ? File.ReadAllLines(filePath)
            : throw new InvalidOperationException($"File does not exist. Parameter name: {nameof(filePath)}");
    }

    public static string WriteFile(string fileName, string fileContent)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        if (string.IsNullOrWhiteSpace(fileContent))
        {
            throw new ArgumentNullException(nameof(fileContent));
        }

        var filePath = GetPath(fileName);
        File.WriteAllText(filePath, fileContent);
        return filePath;
    }

    private static string GetPath(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        return !string.IsNullOrWhiteSpace(assemblyPath)
            ? Path.Combine(assemblyPath, fileName)
            : throw new InvalidOperationException($"Value cannot be null. Parameter name: {nameof(assemblyPath)}");
    }
}
