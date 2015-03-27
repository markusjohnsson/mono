
using MonoTests.System;
using MonoTests.System.Collections.Generic;
using MonoTests.System.Collections.ObjectModel;
using MonoTests.System.IO;
using MonoTests.System.Linq;
using NUnit.Framework;
using System;

namespace Braille.MonoCorlib.Test
{
    class Program
    {

        public static void Main()
        {
            RunTest(typeof(ListTest));
            RunTest(typeof(IListTest));
            RunTest(typeof(DictionaryTest));
            RunTest(typeof(EqualityComparerTest));
            RunTest(typeof(ComparerTest));
            RunTest(typeof(CollectionTest));
            RunTest(typeof(ReadOnlyCollectionTest));
            RunTest(typeof(BufferTest));
            RunTest(typeof(MemoryStreamTest));

            // System.dll
            RunTest(typeof(LinkedListTest));
            RunTest(typeof(QueueTest));
            RunTest(typeof(StackTest));
            RunTest(typeof(SortedDictionaryTest));
            RunTest(typeof(SortedListTest));

            // System.Core.dll
            RunTest(typeof(HashSetTest));
            RunTest(typeof(EnumerableTest));
            RunTest(typeof(EnumerableMoreTest));

            Console.WriteLine("{0}/{1} ({2}%) succeeded", SucceededCount, TotalExecutedCount, 100 * (SucceededCount / (double)TotalExecutedCount));
        }

        private static T FirstOrDefault<T>(T[] source, Func<T, bool> predicate)
        {
            foreach (var s in source)
            {
                if (predicate(s))
                    return s;
            }

            return default(T);
        }

        static int TotalExecutedCount;
        static int SucceededCount;

        private static void RunTest(Type type)
        {
            Console.WriteLine(type.Name);

            var setup = FirstOrDefault(
                type.GetMethods(),
                m => FirstOrDefault(m.GetCustomAttributes(false), a => a.GetType().Name == "SetUpAttribute") != null);

            foreach (var m in type.GetMethods())
            {
                var isTest = false;
                Type expectedException = null;

                foreach (var a in m.GetCustomAttributes(false))
                {
                    if (a.GetType().Name == "TestAttribute")
                        isTest = true;
                    else if (a.GetType().Name == "ExpectedExceptionAttribute")
                        expectedException = ((ExpectedExceptionAttribute) a).ExceptionType;
                }

                if (!isTest)
                    continue;

                var test = Activator.CreateInstance(type);

                TotalExecutedCount++;

                if (setup != null)
                    setup.Invoke(test, new object[0]);

                try
                {
                    m.Invoke(test, new object[0]);

                    SucceededCount++;

                    Console.WriteLine("[ OK ] " + m.Name);
                }
                catch (Exception e)
                {
                    if (expectedException == null || !expectedException.Equals(e.GetType()))
                        Console.WriteLine("[ Exception ] " + m.Name + " " + e.GetType().Name + " " + e.Message);
                    else
                    {
                        Console.WriteLine("[ OK ] " + m.Name);
                        SucceededCount++;
                    }
                }
            }
        }
    }
}
