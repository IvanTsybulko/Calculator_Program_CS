using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //examples
            //11 * 22 + 33 * 11 / 22 + 11                   = 269.5
            //24 + 15 - 21 * 34 / 24 + 12 * 12 + 14 / 14    = 154.25
            //15 + ( 18 + 13 * 8 ) + 2                      = 139
            //15 + ( 18 + 13 * 8 ) + ( 8 + 9 ) + 2          = 156
            //15 + ( 8 * ( 18 + 13 * 8 ) - 9 ) + 2          = 984
            //11 * 22 + 33 * 11 / 22 + 11 + 435 * ( 78 / 13.5 ) = 2782.83333333
            //( ( ( 355 * 256 ) / 33 ) + ( 87 * 4 ) )       = 3101.93939394

            Console.WriteLine("Welcome to the Calculator program!");
            Console.WriteLine("To use the calculator please make sure you are pressing space after each number, operation symbol or bracket!");
            Console.WriteLine("Example: ");
            Console.WriteLine("11 * 22 + 33 * 11 / 22 + 11 + 435 * ( 78 / 13.5 )");
            Console.WriteLine();
            Console.WriteLine("if you want to exit the program type \"exit\"");
            Console.WriteLine();
            Console.WriteLine();


            List<string> problem = Console.ReadLine().Split(' ').ToList();
            Console.WriteLine();

            while (problem[0] != "exit")
            {
                Console.Write(String.Join(" ", problem) + " = ");
                Console.WriteLine(calculate(problem));
                problem = Console.ReadLine().Split(' ').ToList();
                Console.WriteLine();
            }
        }

        private static double calculate(List<string> problem)
        {
            problem = solveBrackets(problem); //solves everithing in brackets
                                              //and replaces it with theresult in the original problem

            problem = solveMultAndDiv(problem); //solves every operation with * or /
                                                //and replaces it with the result in the original problem

            problem = solveAddAndMin(problem); //solves every + or - operation and replaces it with the
                                               //result in the original problem

            return double.Parse(problem[0]);
        }

        private static List<string> solveBrackets(List<string> problem)
        {
            while(problem.Contains("("))
            {
                int openbracket = -1;
                int closeBracket = -1;

                for (int i = 0; i < problem.Count; i++)
                {
                    if (problem[i] == "(")
                    {
                        openbracket = i;
                    }

                    if (problem[i] == ")")
                    {
                        closeBracket = i;
                        break;
                    }
                }

                List<string> inBracketsProblem = problem.Skip(openbracket + 1).Take(closeBracket - openbracket - 1).ToList();
                inBracketsProblem = solveMultAndDiv(inBracketsProblem);
                inBracketsProblem = solveAddAndMin(inBracketsProblem);

                problem[openbracket] = inBracketsProblem[0];

                problem.RemoveRange(openbracket + 1, closeBracket - openbracket);
            }

            return problem;
        }

        private static List<string> solveMultAndDiv(List<string> problem)
        {
            for (int i = 0; i < problem.Count - 1; i++)
            {
                if (problem[i + 1] == "*" || problem[i + 1] == "/")
                {
                    if (problem[i + 1] == "*")
                    {
                        double result = double.Parse(problem[i]) * double.Parse(problem[i + 2]);

                        problem[i] = result.ToString();
                        problem.RemoveAt(i + 1);
                        problem.RemoveAt(i + 1);
                        i--;
                    }
                    else
                    {
                        double result = double.Parse(problem[i]) / double.Parse(problem[i + 2]);

                        problem[i] = result.ToString();
                        problem.RemoveAt(i + 1);
                        problem.RemoveAt(i + 1);
                        i--;
                    }
                }
            }

            return problem;
        }

        private static List<string> solveAddAndMin(List<string> problem)
        {
            for (int i = 0; i < problem.Count - 1; i++)
            {
                if (problem[i + 1] == "+" || problem[i + 1] == "-")
                {
                    if (problem[i + 1] == "+")
                    {
                        double result = double.Parse(problem[i]) + double.Parse(problem[i + 2]);

                        problem[i] = result.ToString();
                        problem.RemoveAt(i + 1);
                        problem.RemoveAt(i + 1);
                        i--;
                    }
                    else
                    {
                        double result = double.Parse(problem[i]) - double.Parse(problem[i + 2]);

                        problem[i] = result.ToString();
                        problem.RemoveAt(i + 1);
                        problem.RemoveAt(i + 1);
                        i--;
                    }
                }
            }

            return problem;
        }

        //double identifyBrackets(List<string> problem)
        //{
        //    List<string> problemBeweenBrackets = new List<string>();

        //    bool betweenBrackets = false;

        //    foreach (var item in problem)
        //    {
        //        if (item == ")")
        //        {
        //            betweenBrackets = false;
        //        }

        //        if (betweenBrackets)
        //        {
        //            problemBeweenBrackets.Add(item);
        //        }

        //        if (item == "(")
        //        {
        //            betweenBrackets = true;
        //        }
        //    }


        //}
    }
}
