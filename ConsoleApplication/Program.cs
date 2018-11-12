using RoslynScriptingAPI;
using System;

namespace ConsoleApplication
{
    class Program
    {
        static Program _instance = new Program();
        static void Main(string[] args)
        {
            _instance.ExecuteSampleCallingMethod();
            Console.ReadKey();
        }

        async void ExecuteSampleCallingMethod()
        {
            // Operation which returns integers
            ExecuteSample executeSample = new ExecuteSample();
            int sum = await executeSample.ReturnIntAsync();

            Console.WriteLine(sum);

            // Operation which change variable provided by calling application
            string initValue = "default Value";
            Console.WriteLine($"For Globals.SomeValue is default:{initValue}");

           string newValue = await executeSample.UseGlobalDataInScriptAsync(initValue,
               "PersonInGlobals.Name = \"Person Name changed inside script\"; SomeVariable = \"new value\";");

            Console.WriteLine($"Inserted script changed value to: {newValue}");

            // Runs given code
            string code = @"int Add(int x, int y) {
                            return x+y;
                            }
                            Add(1, 4)";

           var result = await executeSample.EvaluateScriptAsync(code);
            Console.WriteLine($"Result of code is: {result.ToString()}");
        }
    }
}
