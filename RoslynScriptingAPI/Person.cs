using System;
using System.Collections.Generic;
using System.Text;

namespace RoslynScriptingAPI
{
    public class Person
    {
        public string Name { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();

    }
}
