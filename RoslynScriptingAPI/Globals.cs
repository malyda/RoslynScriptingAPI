using System;
using System.Collections.Generic;
using System.Text;

namespace RoslynScriptingAPI
{
    /// <summary>
    /// Class used for storing data between application and script
    /// </summary>
    public class Globals
    {
        public string SomeVariable { get; set; }
        public Person PersonInGlobals { get; set; }
    }
}
