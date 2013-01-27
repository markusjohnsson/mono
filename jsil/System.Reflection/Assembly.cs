using System;
using System.Collections.Generic;
using System.Text;
using JSIL.Meta;

namespace System.Reflection
{
    public class Assembly
    {
        public string FullName { get; set; }

        [JSExternal]
        public extern static Assembly GetExecutingAssembly();

    }
}
