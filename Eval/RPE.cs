//  Преобразование инфиксной записи к постфиксной
//      i.dudinov@gmail.com
//  Добавлена функция result для вывода результата
// kiss_a@bk.ru
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PostfixNotation
{
    public class PostfixNotationExpression
    {
        public PostfixNotationExpression()
        {
            operators = new List<string>(standart_operators);
            InitializeOperations();
        }
        private List<string> operators;
        private List<string> standart_operators =
            new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^" });

        private Dictionary<string, Func<decimal, decimal, decimal>> operations =
            new Dictionary<string, Func<decimal, decimal, decimal>>();

        private IEnumerable<string> Separate(string input)
        {
            int pos = 0;
            while (pos < input.Length)
            {
                string s = GetString(input, pos);
                yield return s;
                pos += s.Length;
            }
        }

        private string GetString(string input, int pos)
        {
            string s = string.Empty + input[pos];
            if (!standart_operators.Contains(input[pos].ToString()))
            {
                if (Char.IsDigit(input[pos]))
                    for (int i = pos + 1; i < input.Length &&
                        (Char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                        s += input[i];
                else if (Char.IsLetter(input[pos]))
                    for (int i = pos + 1; i < input.Length &&
                        (Char.IsLetter(input[i]) || Char.IsDigit(input[i])); i++)
                        s += input[i];
            }
            return s;
        }

        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default:
                    return 4;
            }
        }

        public string[] ConvertToPostfixNotation(string input)
        {
            List<string> outputSeparated = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Separate(input))
            {
                if (operators.Contains(c))
                {
                    SeparateBraces(outputSeparated, stack, c);
                }
                else
                    outputSeparated.Add(c);
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                    outputSeparated.Add(c);
            return outputSeparated.ToArray();
        }

        private void SeparateBraces(List<string> outputSeparated, Stack<string> stack, string c)
        {
            if (stack.Count > 0 && !c.Equals("("))
                DoWithClosedBraces(outputSeparated, stack, c);
            else
                stack.Push(c);
        }

        private void DoWithClosedBraces(List<string> outputSeparated, Stack<string> stack, string c)
        {
            if (c.Equals(")"))
            {
                string s = stack.Pop();
                while (s != "(")
                {
                    outputSeparated.Add(s);
                    s = stack.Pop();
                }
            }
            else if (GetPriority(c) > GetPriority(stack.Peek()))
                stack.Push(c);
            else
            {
                while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                    outputSeparated.Add(stack.Pop());
                stack.Push(c);
            }
        }

        public decimal result(string input)
        {
            decimal res;
            if (input.Equals(string.Empty) || string.IsNullOrWhiteSpace(input))
                return 0;
            if (decimal.TryParse(input, out res))
                return res;
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = Normalize(input);
            string str = queue.Dequeue();
            DoMaths(ref stack, queue, str);
            return Convert.ToDecimal(stack.Pop());
        }

        private void DoMaths(ref Stack<string> stack, Queue<string> queue, string str)
        {
            while (queue.Count >= 0)
            {
                if (!operators.Contains(str))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    decimal summ = 0;
                    try
                    {
                        summ = CalculateSum(stack, str, summ);
                    }
                    catch (Exception ex) { }
                    stack.Push(summ.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }
            }
        }

        private void InitializeOperations()
        {
            operations["+"] =
                (a, b) => a + b;
            operations["-"] =
                (a, b) => b - a;
            operations["*"] =
                (a, b) => a * b;
            operations["/"] =
                (a, b) => b / a;
        }


        private decimal CalculateSum(Stack<string> stack, string str, decimal summ)
        {
            decimal a = Convert.ToDecimal(stack.Pop());
            decimal b = Convert.ToDecimal(stack.Pop());
            return operations[str](a, b);
        }

        private Queue<string> Normalize(string input)
        {
            return new Queue<string>(ConvertToPostfixNotation(input.Replace(" ", "")
                .Replace("\t", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace(".", ",")));
        }
    }

}