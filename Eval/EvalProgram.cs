using System;
using System.Linq.Expressions;
using System.Text;
using PostfixNotation;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();
		    string output = new PostfixNotationExpression()
		        .result(input).ToString();
			Console.WriteLine(output);
		}
	}
}
