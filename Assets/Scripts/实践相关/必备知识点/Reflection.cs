using System;
using System.Collections.Generic;
using UnityEngine;

public class Father{}
public class Son:Father{}
public class Reflection : MonoBehaviour
{
    Type fatherType = typeof(Father);
    Type sonType = typeof(Son);
    void Start()
    {
        if (fatherType.IsAssignableFrom(sonType))
        {
            Debug.Log("可以装");
            Father f = Activator.CreateInstance(sonType) as Father;
        }
        else
        {
            Debug.Log("不能装");
        }
        
        List<float> list = new List<float>();
        
        Type listType = list.GetType();

        Type[] types =  listType.GetGenericArguments();
        Debug.Log(types[0]);
        
        Dictionary<string,float> dic =  new Dictionary<string,float>();
        Type dicType = dic.GetType();
        types = dicType.GetGenericArguments();
        Debug.Log(types[0]);
        Debug.Log(types[1]);
    }
}
