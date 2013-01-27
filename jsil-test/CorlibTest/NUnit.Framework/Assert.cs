using System;
using System.Collections.Generic;
using System.Text;

namespace NUnit.Framework
{
    public static class Assert
    {
        public static void AreEqual(object expected, object actual, string message)
        {
            if (false == expected.Equals(actual))
                throw new Exception(message);
        }

        public static void AreEqual(object expected, object actual)
        {
            AreEqual(expected, actual, "Assertion failed: " + expected + " != " + actual);
        }

        public static void IsTrue(bool p)
        {
            AreEqual(true, p);
        }

        public static void IsFalse(bool p)
        {
            AreEqual(false, p);
        }

        public static void IsTrue(bool contains, string p)
        {
            AreEqual(true, contains, p);
        }

        public static void IsFalse(bool contains, string p)
        {
            AreEqual(false, contains, p);
        }

        public static void IsNull(object value, string p)
        {
            AreEqual(null, value, p);
        }

        public static void IsNull(object p)
        {
            AreEqual(null, p);
        }

        public static void Fail(string p = null)
        {
            throw new Exception("Test failed: " + p);
        }

        public static void IsNotNull(object p, string message)
        {
            IsTrue(p != null, message);
        }

        public static void AreEqual(double expected, double actual, double epsilon, string message)
        {
            if (double.IsNaN(expected) && double.IsNaN(actual))
                return;

            if (double.IsNaN(expected))
                Assert.Fail(message);

            if (double.IsNaN(actual))
                Assert.Fail(message);

            var diff = Math.Abs(expected - actual);

            if (epsilon < diff)
                Assert.Fail(message);
        }
    }
}
