
using MonoTests.System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Braille.MonoCorlib.Test
{
    class Program
    {
        public static void Main()
        {
            foreach (var m in typeof(ListTest).GetMethods())
            {
                var isTest = false;
                Type expectedException = null;

                foreach (var a in m.GetCustomAttributes(false))
                {
                    if (a.GetType().Name == "TestAttribute")
                        isTest = true;
                    else if (a.GetType().Name == "ExpectedExceptionAttribute")
                        expectedException = ((ExpectedExceptionAttribute)a).ExceptionType;
                }

                if (!isTest)
                    continue;
                
                var test = new ListTest();
                test.SetUp();
                try
                {
                    m.Invoke(test, new object[0]);

                    Console.WriteLine("[ OK ] " + m.Name);
                }
                catch (Exception e) 
                {
                    if (expectedException == null || !expectedException.Equals(e.GetType()))
                        Console.WriteLine("[ Exception ] " + m.Name + " " + e.GetType().Name + " " + e.Message);
                    else
                        Console.WriteLine("[ OK ] " + m.Name);
                }

                
            }
        }
    }
}
