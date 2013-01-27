
using System;
using System.Collections.Generic;
using MonoTests.System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using JSIL.Meta;
using MonoTests.System;
using System.Globalization;

namespace CorlibTest
{
    public class Program
    {
        public static void Main()
        {
            RunTests<TimeSpanTest>(() => new TimeSpanTest());
        }

        private static void RunTests<T>(Func<T> factory) where T: new()
        {
            var dtType = typeof(T);

            var testMethods = new List<MethodInfo>();
            MethodInfo setup = null;

            foreach (var method in dtType.GetMethods())
            {
                if (method.GetCustomAttributes(typeof(TestAttribute), false).Length != 0)
                {
                    testMethods.Add(method);
                }
                else if (method.GetCustomAttributes(typeof(SetUpAttribute), false).Length != 0)
                {
                    setup = method;
                }
            }

            int counter = 0;
            int failedCounter = 0;
            int ignoredCounter = 0;

            foreach (var testInfo in testMethods)
            {
                var dt = factory();
                //dt.SetUp();
                if (setup != null)
                    setup.Invoke(dt, null);

                var expectedException = (ExpectedExceptionAttribute[])
                    testInfo.GetCustomAttributes(typeof(ExpectedExceptionAttribute), false);

                var ignored = (IgnoreAttribute[])
                    testInfo.GetCustomAttributes(typeof(IgnoreAttribute), false);

                counter++;

                if (ignored.Length != 0)
                {
                    ignoredCounter++;
                    Console.WriteLine("Ignored test " + testInfo);
                    continue;
                }

                try
                {
                    testInfo.Invoke(dt, null);

                    if (expectedException.Length > 0) 
                    {
                        Assert.Fail("expected exception " + expectedException[0].ExceptionType);
                    }
                    else
                        Console.WriteLine(testInfo + " : OK");
                }
                catch (Exception e)
                {
                    //var hasx = (bool)JSIL.Verbatim.Expression("expectedException.length == 0");
                    if (expectedException.Length == 0 || expectedException[0].ExceptionType == e.GetType())
                    {
                        failedCounter++;
                        Console.WriteLine(testInfo + " : Failed. " + e.Message);
                    }
                    else 
                    {
                        Console.WriteLine(testInfo + " : OK");
                    }
                }
            }
            Console.WriteLine(counter + " tests total. ");
            Console.WriteLine((counter - failedCounter - ignoredCounter) + " tests succeded.");
            Console.WriteLine(failedCounter + " tests failed.");
            Console.WriteLine(ignoredCounter + " tests ignored.");
        }

    }
}
