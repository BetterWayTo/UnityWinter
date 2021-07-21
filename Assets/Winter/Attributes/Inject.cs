using System;

namespace Winter.Attributes
{
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Method, 
        Inherited = false, 
        AllowMultiple = false)]
    class Inject : Attribute
    {

    }
}
