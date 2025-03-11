using System.Text;

internal class Program
{
    static void Main(string[] args)
    {
        List<string> commands = new List<string>()
    {
       "add",
       "divide",
       "multiply",
       "subtract",
       "modulus"
    };
        Console.WriteLine("Commands: ");
        foreach (var item in commands)
        {
            Console.WriteLine(item);
        }
        Console.Write("Command: ");
        var cmd = ReadLineWithKeywordExpansion(commands);
        Console.Write("Press Enter to Quit");
        Console.ReadLine();
    }

    public static string ReadLineWithKeywordExpansion(List<string> commands)
    {
        int top = Console.CursorTop;
        int left = Console.CursorLeft;

        var sb = new StringBuilder();
        while (true)
        {
            var k = Console.ReadKey(true);
            if (k.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                return sb.ToString();
            }
            else if (k.Key == ConsoleKey.Backspace)
            {
                if (sb.Length > 0)
                {
                    --sb.Length;
                    Console.SetCursorPosition(left, top);
                    Console.Write(sb.ToString() + " ");
                    Console.SetCursorPosition(left + sb.Length, top);
                }
            }
            else if (k.Key == ConsoleKey.Tab)
            {
                int index = 0;
                string candidate = sb.ToString();
                for (int i = (sb.Length - 1); i >= 0; i--)
                {
                    if (sb[i] == ' ')
                    {
                        index = i + 1;
                        candidate = sb.ToString().Substring(index);
                        break;
                    }
                }
                if (candidate != "")
                {
                        List<string> command = commands.Where(cmd => cmd.StartsWith(candidate)).ToList();
                    if (command.Count ==  1)
                    {
                        sb.Length = index;
                        sb.Append(command[0]);
                        Console.SetCursorPosition(left, top);
                        Console.Write(sb.ToString());
                    }
                    else if (command.Count > 1)
                    {
                        top = top + 1;
                        Console.SetCursorPosition(0, top);
                        Console.WriteLine("Available Commands: " + string.Join(" ", command));
                        Console.Write("Command: ");
                        top = top + 1;
                        Console.SetCursorPosition(left, top);
                        Console.Write(sb.ToString());
                    }
                    else
                    {
                        top = top + 1;
                        Console.SetCursorPosition(0, top);
                        Console.WriteLine("No Available Commands ");
                        Console.Write("Command: ");
                        top = top + 1;
                        Console.SetCursorPosition(left, top);
                        Console.Write(sb.ToString());
                    }
                }
            }
            else if (k.KeyChar != '\0') // Ignore special keys.
            {
                sb.Append(k.KeyChar);
                Console.SetCursorPosition(left, top);
                Console.Write(sb.ToString());
            }
        }
    }
}
