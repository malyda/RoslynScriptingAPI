using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoslynScriptingAPI
{
    /// <summary>
    /// https://github.com/dotnet/roslyn/wiki/Scripting-API-Samples#multi
    /// https://blogs.msdn.microsoft.com/cdndevs/2015/12/01/adding-c-scripting-to-your-development-arsenal-part-1/
    /// </summary>
    public class ExecuteSample
    {
        public async Task<int> ReturnIntAsync(string sum = "1 + 2")
        {
            return await CSharpScript.EvaluateAsync<int>(sum);
        }

        /// <summary>
        /// Use property from sender object (Globals.cs) in script
        /// </summary>
        /// <param name="defaultValue">Set init value for property</param>
        /// <param name="code">Code interacting with properties of Globals.cs</param>
        /// <returns></returns>
        public async Task<string> UseGlobalDataInScriptAsync(string defaultValue = "Person default name",
            string code = 
            "PersonInGlobals.Name = \"Person Name changed inside script\"; " +
            "SomeVariable = \"new value\";"+
            "PersonInGlobals.Items.Add(new Item(){ItemName =\"Item Name\"});")
        {
            Globals globals = new Globals();
            globals.SomeVariable = defaultValue;
            globals.PersonInGlobals = new Person();

            var metadata = MetadataReference.CreateFromFile(globals.GetType().Assembly.Location);
            try
            {
                await CSharpScript.RunAsync(
                    code
                    , options: ScriptOptions.Default
                            .WithReferences(metadata)
                            .WithImports(GetType().Namespace)
                    , globals: globals);

                return $"SomeVariable value: {globals.SomeVariable} PersonInGlobals.Name value = {globals.PersonInGlobals.Name}";
            }
            catch (CompilationErrorException e)
            {
                return string.Join(Environment.NewLine, e.Diagnostics);
            }
        }

        /// <summary>
        /// Runs given code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<object> EvaluateScriptAsync(string code = @"int Add(int x, int y) {
                                                            return x+y;
                                                            }
                                                            Add(1, 4)")
        {
            try
            {
                return await CSharpScript.EvaluateAsync(code);
            }
            catch (CompilationErrorException e)
            {
                return string.Join(Environment.NewLine, e.Diagnostics);
            }
        }
    }
}
