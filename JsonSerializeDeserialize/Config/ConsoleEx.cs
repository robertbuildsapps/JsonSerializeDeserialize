namespace JsonSerializeDeserialize.Config;

internal static class ConsoleEx
{
    public static void WriteLine(
        string value,
        int backgroundColor = 0,
        bool lineAfter = false,
        bool lineBefore = false)
    {
        switch (backgroundColor)
        {
            case 1:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
            case 2:
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Black;
                break;
        }

        if (lineBefore)
        {
            Console.WriteLine(String.Empty);
        }

        Console.WriteLine(value);

        if (lineAfter)
        {
            Console.WriteLine(String.Empty);
        }
    }
}
