namespace JsonSerializeDeserialize.Domain;

internal class Item : IEquatable<Item?>
{
    public string Name { get; set; } = default!;

    public string Category { get; set; } = default!;

    public string Price { get; set; } = default!;

    public override bool Equals(object? obj)
    {
        return Equals(obj as Item);
    }

    public bool Equals(Item? other)
    {
        return other is not null &&
               Name == other.Name &&
               Category == other.Category &&
               Price == other.Price;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Category, Price);
    }
}
