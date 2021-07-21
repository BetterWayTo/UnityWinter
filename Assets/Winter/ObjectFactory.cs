using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using Winter.Attributes;

namespace Winter
{
    public class ObjectFactory
    {
        public object GetBean(string name)
        {            
            return m_BeansContainer[name];
        }

        public T GetBean<T>()
        {
            return (T)GetBean(typeof(T).Name);
        }

        public void AddBean(string name, object value)
        {
            if (m_BeansContainer.ContainsKey(name))
            {
                throw new WinterException("bean has already existed with same name: " + name);
            }
            m_BeansContainer.Add(name, value);
        }

        public void CreateBeans()
        {            
            foreach (var beanDefinition in m_BeanDefinitionsContainer)
            {                
                Type type = Type.GetType(beanDefinition.Value.BeanClassName);
                var ctors = type.GetConstructors();
                var list = new List<ConstructorInfo>(ctors);
                var ctor = list.FirstOrDefault(c => c.GetParameters().Length == 0);
                object result = null;
                if (ctor != null || ctors.Count() == 0)
                {
                    result = Activator.CreateInstance(type);                    
                }
                else
                {
                    //TODO: with parameters
                }

                //TODO: init and destroy methods
                if (result != null)
                {
                    AddBean(beanDefinition.Key, result);
                }
                else
                {
                    throw new WinterException("error in creation bean, can't find suitable constructor for: " +
                       beanDefinition.Key);
                }
            }
        }

        public void ConfigureBeans()
        {
            foreach (var bean in m_BeansContainer)
            {
                foreach (var field in bean
                    .Value
                    .GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {

                    foreach (var attr in Attribute.GetCustomAttributes(field))
                    {
                        if (attr.GetType() == typeof(Inject))
                        {                            
                            var b = GetBean(field.FieldType.FullName);
                            field.SetValue(bean.Value, b);
                        }
                    }
                }
            }
        }

        public void AddBeanDefinition(string name, string implementation, string scope)
        {
            var beanDefinition = new BeanDefinition();
            beanDefinition.BeanClassName = implementation;
            beanDefinition.BeanName = name;
            beanDefinition.Scope = scope;
            m_BeanDefinitionsContainer.Add(name, beanDefinition);
        }

        private Dictionary<string, object> m_BeansContainer 
            = new Dictionary<string, object>();
        private Dictionary<string, BeanDefinition> m_BeanDefinitionsContainer 
            = new Dictionary<string, BeanDefinition>();
    }
}