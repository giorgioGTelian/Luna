using System;

public class Token
{
    public enum TokenType
    {
        WHITESPACE,
        LINE_END,
        IDENTIFIER,
        LITERAL,
        STRING_LITERAL,
        OPERATOR,
        LINE_COMMENT,
        BLOCK_COMMENT,
        SCOPE,
        UNKNOWN
    }

    public string Text { get; }
    public int Line { get; }
    public int CharPos { get; }
    public TokenType Type { get; }
    public Operator Op { get; }

    public Token(string text, int line = 0, int charPos = 0, TokenType type = TokenType.IDENTIFIER, Operator op = null)
    {
        Text = text;
        Line = line;
        CharPos = charPos;
        Type = type;
        Op = op;
    }

    public string GetDescription()
    {
        return $"{Line}:{CharPos} ({TypeToString(Type)} '{Text}')";
    }

    public string GetTypeDescription()
    {
        string opDescription = Type == TokenType.OPERATOR && Op != null ? $"{Op.GetText()} " : "unknown ";
        return opDescription + TypeToString(Type);
    }

    private string TypeToString(TokenType type)
    {
        return type switch
        {
            TokenType.WHITESPACE => "whitespace",
            TokenType.LINE_END => "newline",
            TokenType.IDENTIFIER => "identifier",
            TokenType.LITERAL => "literal",
            TokenType.STRING_LITERAL => "string literal",
            TokenType.OPERATOR => "operator",
            TokenType.LINE_COMMENT => "single line comment",
            TokenType.BLOCK_COMMENT => "block comment",
            TokenType.SCOPE => "scope",
            TokenType.UNKNOWN => "UNKNOWN",
            _ => "ERROR GETTING TOKEN TYPE"
        };
    }
}

