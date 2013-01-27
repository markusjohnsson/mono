using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.Framework
{
    public class TestAttribute : Attribute
    { 
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(string key)
        {

        }
    }
}