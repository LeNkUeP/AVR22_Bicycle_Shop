using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine
{
    public class SerializeFieldAttribute : Attribute
    {
    }

    public class RequireComponentAttribute : Attribute
    {
        public RequireComponentAttribute(Type type) { }
    }
}
