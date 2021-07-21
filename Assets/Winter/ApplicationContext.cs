using System;
using System.Reflection;
using Winter.Attributes;

namespace Winter
{
    public class ApplicationContext
    {
        public object GetBean(string name)
        {
            return m_ObjectFactory.GetBean(name);
        }

        public T GetBean<T>()
        {
            return m_ObjectFactory.GetBean<T>();
        }

        public void AddBeanDefinition(string name, string implementation, string scope)
        {            
            m_ObjectFactory.AddBeanDefinition(name, implementation, scope);
        }

        private void ConfiguratorHandler(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                foreach (var attr in Attribute.GetCustomAttributes(method))
                {
                    if (attr.GetType() == typeof(Bean))
                    { 
                        AddBeanDefinition(((Bean)attr).type1.FullName, 
                            ((Bean)attr).type2.FullName, 
                            BeanDefinition.SingeltonScope);
                    }
                }
            }
        }

        private void ControllerHandler(Type type)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Length > 0)
            {
                AddBeanDefinition(interfaces[0].FullName, type.FullName, BeanDefinition.SingeltonScope);
            }
            else
            {
                AddBeanDefinition(type.FullName, type.FullName, BeanDefinition.SingeltonScope);
            }
        }

        private void CreateBeans()
        {
            m_ObjectFactory.CreateBeans();
        }

        private void ConfigureBeans()
        {
            m_ObjectFactory.ConfigureBeans();
        }

        public void InitContext()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(Configurator), true).Length > 0)
                    {
                        ConfiguratorHandler(type);
                    }
                }
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(Controller), true).Length > 0)
                    {                        
                        ControllerHandler(type);
                    }
                }
            }

            CreateBeans();
            ConfigureBeans();
        }

        private ObjectFactory m_ObjectFactory = new ObjectFactory();
    }
}