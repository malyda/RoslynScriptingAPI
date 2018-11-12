using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Threading.Tasks;

namespace RoslynScriptingAPI
{
    /// <summary>
    /// https://github.com/dotnet/roslyn/wiki/Scripting-API-Samples#multi
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
        public async Task<string> UseGlobalDataInScriptAsync(string defaultValue = "now not changed",
            string code = "SomeVariable = \"Changed inside script\";")
        {
            Globals globals = new Globals();
            globals.SomeVariable = defaultValue;

            await CSharpScript.RunAsync(
                code
                , globals: globals);

            return globals.SomeVariable;
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
