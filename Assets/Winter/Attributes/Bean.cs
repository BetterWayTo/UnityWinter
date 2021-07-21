using System;

namespace Winter.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    class Bean : Attribute
    {
        public Type type1, type2;

        public Bean(Type t1, Type t2)
        {
            type1 = t1;
            type2 = t2;
        }
    }
}

