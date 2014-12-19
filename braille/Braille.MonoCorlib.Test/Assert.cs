
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
                Fail("Assertion failed: " + expected + " != " + actual, message);
        }

        public static void AreEqual(object expected, object actual, string message = null)
        {
            if (!expected.Equals(actual))
                Fail("Assertion failed: " + expected + " != " + actual, message);
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
    }
}
