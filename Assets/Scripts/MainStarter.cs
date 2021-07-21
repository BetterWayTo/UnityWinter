using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Winter;

public class MainStarter : MonoBehaviour
{
    private ApplicationContext m_ApplicationContext = new ApplicationContext();

    void Start()
    {
        m_ApplicationContext.InitContext();
        var bean = m_ApplicationContext.GetBean<InjectSomeInterface>();
        Debug.Log(bean.SomeInterface);
    }

    
}
