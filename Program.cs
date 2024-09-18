using System;
using System.ComponentModel.Design;
using System.Data;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till den textbaserade kalkylatorn!");
        Console.WriteLine("Ange ett matematiskt uttryck (eller skriv 'exit' för att avsluta):");

        while (true)
        {
            string input = Console.ReadLine();

            if (input.Trim().ToLower() == "exit")
            {
                break;
            }

            try
            {
                var result = EvaluateExpression(input);
                Console.WriteLine($"Resultat : {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel : " + ex.Message);
            }
        }
    }
    
    static double EvaluateExpression(string expression)
    {
        var tokens = new List<string>();

        int i = 0;
        while (i < expression.Length)
        {
            if (char.IsDigit(expression[i]) || expression[i] == '.')
            {
                string number = "";

                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.')) 
                {
                    number += expression[i];
                    i++;

                } 
                tokens.Add(number);
            }
            else if ("+-*/".Contains(expression[i]))
            {
                tokens.Add(expression[i].ToString());
                i++;
            }
            else
            {
                i++;
            }

        }

        for ( i = 0; i < tokens.Count; i++)
        {
            if (tokens[i] == "*" || tokens[i] == "/")
            {
                double left = Convert.ToDouble(tokens[i - 1]);
                double right = Convert.ToDouble(tokens[i + 1]);
                double result = tokens[i] == "*" ? left * right : left / right;

                tokens[i - 1] = result.ToString();
                tokens.RemoveAt(i);
                tokens.RemoveAt(i);
                i--;
            }
        }

        double total = Convert.ToDouble(tokens[0]);
        for (i = 1; i < tokens.Count; i += 2)
        {
            string op = tokens[i];
            double nextNumber = Convert.ToDouble(tokens[i + 1]);

            if (op == "+")
            {
                total += nextNumber;
            }
            else if (op == "-") 
            {
                 total -= nextNumber;
            }
        }

        return total;
    }
}