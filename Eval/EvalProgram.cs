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
		    string output = new PostfixNotationExpression().result(input).ToString();
			Console.WriteLine(output);

		}

	    public class RPEParser
	    {
            private char[] separators = new char[]
            {
                '+',
                '-',
                '*',
                '/',
                '(',
                ')'
            };

	        public string FromExpression(string input)
	        {
	            var result = new StringBuilder();
	            foreach (var symbol in input.Remove(' '))
	            {
	                
	            }

	            return null;
	        }
	    }
	}
}
