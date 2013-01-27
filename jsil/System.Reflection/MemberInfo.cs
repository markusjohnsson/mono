using System;
using System.Collections.Generic;
using System.Text;

namespace System.Reflection
{
    public abstract class MemberInfo
    {
        public extern object[] GetCustomAttributes(Type attributeType, bool inherit);

        public extern object[] GetCustomAttributes(bool inherit);
    }
}
