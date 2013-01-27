using System;

namespace NUnit.Framework
{
    public class ExpectedExceptionAttribute : Attribute
    {
        public Type ExceptionType { get; private set; }

        public ExpectedExceptionAttribute(Type exceptionType)
        {
            ExceptionType = exceptionType;
        }
    }
}
