using System;

public class ErrorHandler
{
    public enum ErrorPriority
    {
        SOURCE_ERROR,
        SOURCE_WARNING,
        JSYK,
        INTERNAL_ERROR,
        RUNTIME_ERROR
    }

    public bool ErrorHasBeenLogged { get; private set; }

    public static string PriorityToStr(ErrorPriority priority)
    {
        return priority switch
        {
            ErrorPriority.SOURCE_ERROR => "error",
            ErrorPriority.SOURCE_WARNING => "warning",
            ErrorPriority.JSYK => "jsyk",
            ErrorPriority.INTERNAL_ERROR => "INTERNAL ERROR",
            ErrorPriority.RUNTIME_ERROR => "runtime error",
            _ => "UNKNOWN PRIORITY LEVEL"
        };
    }

    public void Log(string msg, ErrorPriority priority, Token token = null)
    {
        if (priority == ErrorPriority.SOURCE_ERROR ||
            priority == ErrorPriority.INTERNAL_ERROR ||
            priority == ErrorPriority.RUNTIME_ERROR)
        {
            ErrorHasBeenLogged = true;
        }

        Console.WriteLine(PriorityToStr(priority));

        if (token != null)
        {
            Console.WriteLine($" in '{token.File?.Filename}' on line {token.Line}:");
            Console.WriteLine(IndentString(msg, "    "));

            string line = token.File?.GetLine(token.Line) ?? "";

            int wspace = 0;
            while (wspace < line.Length && char.IsWhiteSpace(line[wspace]))
            {
                wspace++;
            }

            string arrows = new string(' ', token.CharPos - 1 - wspace) +
                            new string('^', token.Text.Length);

            Console.WriteLine(IndentString(line.Substring(wspace) + "\n" + arrows, "    "));
        }
        else
        {
            Console.WriteLine(": " + msg);
        }
    }

    public static string IndentString(string str, string indent)
    {
        return str.Replace("\n", "\n" + indent);
    }
}

