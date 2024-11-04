using System;
using System.Collections.Generic;

public class AllOperators
{
	private static AllOperators ops = null;
	private Dictionary<string, Operator> opsMap = new Dictionary<string, Operator>();

	public static void Init()
	{
		ops = new AllOperators();
	}

	private AllOperators()
	{
		#undef DECLARE_OP
		#define DECLARE_OP(name, text, prece, input, overload) PutOpInMap(name);
		ALL_OPS;
	}

	private void PutOpInMap(Operator op)
	{
		opsMap[op.GetText()] = op;
	}

	public void Get(string text, List<Operator> outList)
	{
		int start = 0;
		int end = text.Length;

		while (start < text.Length)
		{
			while (true)
			{
				if (end <= start)
				{
					ErrorHandler.Log("unknown operator '" + text + "'", ErrorHandler.SourceError);
				}

				var substr = text.Substring(start, end - start);
				if (!opsMap.TryGetValue(substr, out var op))
				{
					end--;
				}
				else
				{
					outList.Add(op);
					start = end;
					end = text.Length;
					break;
				}
			}
		}
	}

	public bool IsOpenBrac(Operator op)
	{
		return op == Operator.OpenPeren || op == Operator.OpenSqBrac || op == Operator.OpenCrBrac;
	}

	public bool IsCloseBrac(Operator op)
	{
		return op == Operator.ClosePeren || op == Operator.CloseSqBrac || op == Operator.CloseCrBrac;
	}

	public static Operator OpCreate(string textIn, int leftPrecedenceIn, int rightPrecedenceIn, bool overloadableIn)
	{
		var op = new Operator(textIn, leftPrecedenceIn, rightPrecedenceIn, overloadableIn);

		InsertPrecedenceLevel(OperatorData.PrecedenceLevels, leftPrecedenceIn);
		InsertPrecedenceLevel(OperatorData.PrecedenceLevels, rightPrecedenceIn);

		return op;
	}

	private static void InsertPrecedenceLevel(List<int> precedenceLevels, int precedence)
	{
		for (int i = 0; i < precedenceLevels.Count; i++)
		{
			if (precedenceLevels[i] > precedence)
			{
				precedenceLevels.Insert(i, precedence);
				return;
			}
			else if (precedenceLevels[i] == precedence)
			{
				return;
			}
		}
		precedenceLevels.Add(precedence);
	}
}

public class Operator
{
	public string Text { get; }
	public int LeftPrecedence { get; }
	public int RightPrecedence { get; }
	public bool Overloadable { get; }

	public Operator(string text, int leftPrecedence, int rightPrecedence, bool overloadable)
	{
		Text = text;
		LeftPrecedence = leftPrecedence;
		RightPrecedence = rightPrecedence;
		Overloadable = overloadable;
	}

	public string GetText()
	{
		return Text;
	}

	public static Operator OpenPeren { get; } = new Operator("(", 0, 0, false);
	public static Operator OpenSqBrac { get; } = new Operator("[", 0, 0, false);
	public static Operator OpenCrBrac { get; } = new Operator("{", 0, 0, false);
	public static Operator ClosePeren { get; } = new Operator(")", 0, 0, false);
	public static Operator CloseSqBrac { get; } = new Operator("]", 0, 0, false);
	public static Operator CloseCrBrac { get; } = new Operator("}", 0, 0, false);
}

public static class ErrorHandler
{
	public const int SourceError = 1;

	public static void Log(string message, int errorType)
	{
		Console.WriteLine($"Error: {message}, Type: {errorType}");
	}
}

public static class OperatorData
{
	public static List<int> PrecedenceLevels { get; } = new List<int>();
}