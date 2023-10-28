using System;

public enum ErrorPriority
{
    SOURCE_ERROR,
    SOURCE_WARNING,
    JSYK,
    INTERNAL_ERROR,
    RUNTIME_ERROR
}

public class ErrorHandler
{
    public bool ErrorHasBeenLogged { get; private set; }

    public static string PriorityToStr(ErrorPriority inPriority)
    {
        switch (inPriority)
        {
            case ErrorPriority.SOURCE_ERROR:
                return "error";
            case ErrorPriority.SOURCE_WARNING:
                return "warning";
            case ErrorPriority.JSYK:
                return "jsyk";
            case ErrorPriority.INTERNAL_ERROR:
                return "INTERNAL ERROR";
            case ErrorPriority.RUNTIME_ERROR:
                return "runtime error";
            default:
                return "UNKNOWN PRIORITY LEVEL";
        }
    }

    public void Log(string msg, ErrorPriority priority, Token token)
    {
        if (priority == ErrorPriority.SOURCE_ERROR ||
            priority == ErrorPriority.INTERNAL_ERROR ||
            priority == ErrorPriority.RUNTIME_ERROR)
        {
            ErrorHasBeenLogged = true;
        }

        // Luna style
        Console.Write(PriorityToStr(priority));

        if (token != null)
        {
            Console.WriteLine($" in '{token.File.Filename}' on line {token.Line}:");
            Console.WriteLine(IndentString(msg, "    "));

            string line = token.File.GetLine(token.Line);

            int wspace = 0;
            while (wspace < line.Length && (line[wspace] == ' ' || line[wspace] == '\t' || line[wspace] == '\n'))
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

    public void Msg(string inMsg)
    {
        Console.WriteLine("message: " + inMsg);
    }

    public static string IndentString(string str, string indent)
    {
        return str.Replace("\n", "\n" + indent);
    }
}

public class PineconeError
{
    private string Msg { get; }
    private ErrorPriority Priority { get; }
    private Token Token { get; }

    public PineconeError(string msgIn, ErrorPriority priorityIn, Token tokenIn)
    {
        Msg = msgIn;
        Priority = priorityIn;
        Token = tokenIn;
    }

    public void Log(ErrorHandler error)
    {
        error.Log(Msg, Priority, Token);
    }
}

// Token class and related classes would need to be defined based on your specific requirements.



/* written in c++ but does not work: */
/*
#include "../h/ErrorHandler.h"
#include "../h/msclStringFuncs.h"
#include "../h/SourceFile.h"

#include <iostream>

using std::cout;
using std::endl;

ErrorHandler error;

string ErrorHandler::priorityToStr(ErrorPriority in)
{
	switch (in)
	{
	case SOURCE_ERROR:
		return "error";
		break;
		
	case SOURCE_WARNING:
		return "warning";
		break;
		
	case JSYK:
		return "jsyk";
		break;
		
	case INTERNAL_ERROR:
		return "INTERNAL ERROR";
		break;
		
	case RUNTIME_ERROR:
		return "runtime error";
		break;
		
	default:
		return "UNKNOWN PRIORITY LEVEL";
		break;
	}
}

void ErrorHandler::log(string msg, ErrorPriority priority, Token token)
{
	if (priority==SOURCE_ERROR || priority==INTERNAL_ERROR || priority==RUNTIME_ERROR)
		errorHasBeenLogged=true;
	
	// gcc style
	//if (token)
	//	cout << token->getFile() << ":" << token->getLine() << ":" << token->getCharPos() << ": ";
	
	//cout << priorityToStr(priority) << ": " << msg << endl;
	
	// Luna style
	
	cout << priorityToStr(priority);
	
	if (token)
	{
		cout << " in '" << token->getFile()->getFilename() << "' on line " << token->getLine() << ":" << endl;
		cout << indentString(msg, "    ") << endl;
		
		string line=token->getFile()->getLine(token->getLine());
		
		int wspace=0;
		for (; wspace<int(line.size()) && (line[wspace]==' ' || line[wspace]=='\t' || line[wspace]=='\n'); wspace++) {}
		
		string arrows="";
		for (int i=0; i<token->getCharPos()-1-wspace; i++)
			arrows+=" ";
		for (int i=0; i<int(token->getText().size()); i++)
			arrows+="^";
		
		cout << indentString(""+line.substr(wspace, string::npos)+"\n"+arrows, "    ") << endl;
	}
	else
	{
		cout << ": " << msg << endl;
	}
}

void ErrorHandler::msg(string in)
{
	cout << "message: " << in << endl;
}

PineconeError::PineconeError(string msgIn, ErrorPriority priorityIn, Token tokenIn)
{
	msg=msgIn;
	priority=priorityIn;
	token=tokenIn;
}

void PineconeError::log()
{
	error.log(msg, priority, token);
}
*/
