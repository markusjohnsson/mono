
using System;
namespace NUnit.Framework
{
    public class TestAttribute : Attribute
    {
    }

    public class TestFixtureAttribute : Attribute
    {
    }

    public class SetUpAttribute : Attribute
    {
    }

    public class TearDownAttribute : Attribute
    {
    }

    public class ExpectedExceptionAttribute : Attribute
    {
        public ExpectedExceptionAttribute(Type exceptionType)
        {
            ExceptionType = exceptionType;
        }

        public Type ExceptionType { get; set; }
    }

    public class Assert
    {
        public static void AreEqual(int expected, int actual, string message = null)
        {
            if (expected != actual)
                Fail("Assertion AreEqual failed: " + expected + " != " + actual, message);
        }

        public static void AreEqual(long expected, long actual, string message = null)
        {
            if (expected != actual)
                Fail("Assertion AreEqual failed: " + expected + " != " + actual, message);
        }

        public static void AreEqual(float expected, float actual, string message = null)
        {
            if (expected != actual)
                Fail("Assertion AreEqual failed: " + expected + " != " + actual, message);
        }

        public static void AreEqual(double expected, double actual, string message = null)
        {
            if (expected != actual)
                Fail("Assertion AreEqual failed: " + expected + " != " + actual, message);
        }

        public static void AreNotEqual(int expected, int actual, string message = null)
        {
            if (expected == actual)
                Fail("Assertion AreNotEqual failed: " + expected + " == " + actual, message);
        }

        public static void AreNotEqual(long expected, long actual, string message = null)
        {
            if (expected == actual)
                Fail("Assertion AreNotEqual failed: " + expected + " == " + actual, message);
        }

        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (expected == null && actual == null)
                return;

            if (expected is long && actual is int)
            {
                AreEqual((long)expected, (long)(int)actual, message);
            }
            else if (expected is int && actual is long)
            {
                AreEqual((long)(int)expected, (long)actual, message);
            }
            else if (expected is float && actual is int)
            {
                AreEqual((float)(int)expected, (float)actual, message);
            }
            else if (expected is int && actual is float)
            {
                AreEqual((float)(int)expected, (float)actual, message);
            }
            else if (expected is double && actual is int)
            {
                AreEqual((double)(int)expected, (double)actual, message);
            }
            else if (expected is int && actual is double)
            {
                AreEqual((double)(int)expected, (double)actual, message);
            }
            else if (expected == null || !expected.Equals(actual))
            {
                Fail("Assertion failed: " + expected + " != " + actual, message);
            }
        }

        public static void Fail()
        {
            Fail("Assertion failed");
        }

        public static void Fail(string message, string message2 = null)
        {
            throw new Exception(message + " " + message2);
        }

        public static void IsTrue(bool p, string message = null)
        {
            if (!p)
                Fail("Assertion failed", message);
        }

        public static void IsFalse(bool p, string message = null)
        {
            if (p)
                Fail("Assertion failed", message);
        }

        public static void IsNotNull(object o, string message = null)
        {
            if (o == null)
                Fail("Assertion failed", message);
        }

        public static void IsNull(object o, string message = null)
        {
            if (o != null)
                Fail("Assertion failed", message);
        }

        public static void AreSame(object a, object b, string message = null)
        {
            if (!object.ReferenceEquals(a, b))
                Fail("Assertion failed", message);
        }
    }
}
