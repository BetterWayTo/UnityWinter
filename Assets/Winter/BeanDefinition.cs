using System.Collections.Generic;

namespace Winter
{
    public class BeanDefinition
    {
        public const string PrototypeScope = "Prototype";
        public const string SingeltonScope = "Singelton";

        public string BeanClassName { get => m_BeanClassName; set => m_BeanClassName = value; }
        public string DestroyMethodName { get => m_DestroyMethodName; set => m_DestroyMethodName = value; }
        public string InitMethodName { get => m_InitMethodName; set => m_InitMethodName = value; }
        public string OriginatingBeanDefinition { get => m_OriginatingBeanDefinition; set => m_OriginatingBeanDefinition = value; }
        public string Scope { get => m_Scope; set => m_Scope = value; }
        public List<Property> Properties { get => m_Properties; set => m_Properties = value; }
        public string BeanName { get => m_BeanName; set => m_BeanName = value; }
        public bool IsConfigured { get => m_IsConfigured; set => m_IsConfigured = value; }

        private string m_BeanClassName;
        private string m_DestroyMethodName;
        private string m_InitMethodName;
        private string m_OriginatingBeanDefinition;
        private string m_Scope;
        private List<Property> m_Properties;
        private string m_BeanName;
        private bool m_IsConfigured;
    }
}