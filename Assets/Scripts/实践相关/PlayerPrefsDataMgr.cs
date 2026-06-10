using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class PlayerPrefsDataMgr
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();
    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }
    private  PlayerPrefsDataMgr()
    {}

    public void SaveData(object data, string keyName)
    {
        if (data != null)
        {
            //自定义KeyName
            //KeyName_数据类型_字段类型_字段名字
            Type type = data.GetType();
            FieldInfo[] infos = type.GetFields();
            string saveKeyName;
            for (int i = 0; i < infos.Length; i++)
            {
                FieldInfo fieldInfo = infos[i];
                saveKeyName = keyName+"_"+type.Name+"_"+fieldInfo.FieldType.Name+"_"+fieldInfo.Name;
                //通过反射获取data中的fieldInfo对应字段
                SaveValue(fieldInfo.GetValue(data), saveKeyName);
            }
            PlayerPrefs.Save();
        }
    }

    private void SaveValue(object value, string keyName)
    {
        if (value != null)
        {
            Type fieldType = value.GetType();
            if (fieldType == typeof(int))
            {
                PlayerPrefs.SetInt(keyName, (int)value);
            }else if (fieldType == typeof(float))
            {
                PlayerPrefs.SetFloat(keyName, (float)value);
            }else if (fieldType == typeof(string))
            {
                PlayerPrefs.SetString(keyName,value.ToString());
            }else if (fieldType == typeof(bool))
            {
                PlayerPrefs.SetInt(keyName,(bool)value ? 1 : 0);
            }else if (typeof(IList).IsAssignableFrom(fieldType))
            {
                IList list = value as IList;
                PlayerPrefs.SetInt(keyName,list.Count);
                int index = 0;
                foreach (object obj in list)
                {
                    SaveValue(obj, keyName+index);
                    index++;
                }
            }else if (typeof(IDictionary).IsAssignableFrom(fieldType))
            {
                IDictionary dic = value as IDictionary;
                PlayerPrefs.SetInt(keyName,dic.Count);
                int index = 0;
                foreach (object obj in dic.Keys)
                {
                    SaveValue(obj, keyName+"_key_"+index);
                    SaveValue(dic[obj], keyName+"_value_"+index);
                    index++;
                }
            }
            else
            {
                SaveData(value, keyName);
            }
        }
        
    }
    public object LoadData(Type type, string keyName)
    {
        object data = Activator.CreateInstance(type);
        FieldInfo[] infos = type.GetFields();
        string loadKeyName;
        FieldInfo fieldInfo;
        for (int i = 0; i < infos.Length; i++)
        {
            fieldInfo = infos[i];
            loadKeyName = keyName+"_"+type.Name+"_"+fieldInfo.FieldType.Name+"_"+fieldInfo.Name;
            fieldInfo.SetValue(data,LoadValue(fieldInfo.FieldType, loadKeyName));
        }
        return data;
    }

    private object LoadValue(Type data, string keyName)
    {
        if (data != null)
        {
            if (data == typeof(int))
            {
                return PlayerPrefs.GetInt(keyName);
            }else if (data == typeof(float))
            {
                return PlayerPrefs.GetFloat(keyName);
            }else if (data == typeof(string))
            {
                return PlayerPrefs.GetString(keyName);
            }else if (data == typeof(bool))
            {
                return PlayerPrefs.GetInt(keyName) == 1 ? true : false;
            }else if (typeof(IList).IsAssignableFrom(data))
            {
                int count = PlayerPrefs.GetInt(keyName);
                IList list = Activator.CreateInstance(data) as IList;
                for (int i = 0; i < count; i++)
                {
                    list.Add(LoadValue(data.GetGenericArguments()[0], keyName + i));
                }
                return list;
            }else if (typeof(IDictionary).IsAssignableFrom(data))
            {
                int count = PlayerPrefs.GetInt(keyName);
                IDictionary dic = Activator.CreateInstance(data) as IDictionary;
                for (int i = 0; i < count; i++)
                {
                    dic.Add(
                        LoadValue(data.GetGenericArguments()[0], keyName+"_key_" + i),
                        LoadValue(data.GetGenericArguments()[1], keyName+"_value_" + i));
                }
                return dic;
            }
            else
            {
                return  LoadData(data,keyName);
            }
        }
        return null;
    }
}
