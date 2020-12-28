using System;
using System.Runtime.CompilerServices;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            bool isRunning = true;
                        
            // welcome message
            Console.WriteLine("********************************");
            Console.WriteLine("WELCOME to the C# CLI Calculator");
            Console.WriteLine("********************************\n");

            while (isRunning)
            {
                // set variables
                double result = 0;

                // first number input
                double firstNumber = GetNumericInput("first");

                // second number input
                double secondNumber = GetNumericInput("second");

                // get operator
                char operater = GetOperatorInput();   // this misspelling in the variable name is intentional, as "operator" is a reserved word

                if (operater == '/' && secondNumber == 0)
                {
                    Console.WriteLine("Division by zero not possible, choose a different operator.");
                    operater = GetOperatorInput();
                }

                result = DoMath(firstNumber, secondNumber, operater);

                Console.WriteLine($"\n{firstNumber} {operater} {secondNumber} = {result:0.##}"); // make sure 

                Console.WriteLine("\nEnter 0 to exit, any other key to continue: ");
                if (Console.ReadLine() == "0")
                {
                    isRunning = false;
                }
            }                
        }

        private double GetNumericInput(string cardinality)
        {
            // set variables
            string input = "";
            double result = 0;

            // get input
            Console.Write("Enter the " + cardinality + " number: ");
            input = Console.ReadLine();

            while (!double.TryParse(input, out result))
            {
                Console.Write("Invalid input. Enter a number: ");
                input = Console.ReadLine();
            }
            return result;
        }

        private char GetOperatorInput()
        {
            // set variables
            string input = "";
            char result = '\0';

            // get input
            Console.WriteLine("Choose an operator:");
            Console.WriteLine("\t +");
            Console.WriteLine("\t -");
            Console.WriteLine("\t *");
            Console.WriteLine("\t /");
            Console.Write("Selection: ");

            input = Console.ReadLine();

            while (!char.TryParse(input, out result) || (result != '+' && result != '-' && result != '*' && result != '/'))
            {
                Console.Write("Invalid input. Choose an operator: ");
                input = Console.ReadLine();
            }
            return result;
        }

        private double DoMath(double firstNumber, double secondNumber, char operater)
        {
            double result = double.NaN; 
            switch (operater)
            {
                case '+':
                    result = firstNumber + secondNumber;
                    break;
                case '-':
                    result = firstNumber - secondNumber;
                    break;
                case '*':
                    result = firstNumber * secondNumber;
                    break;
                case '/':
                    result = firstNumber / secondNumber;
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}