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
                Debug.Log(keyName);
                PlayerPrefs.SetInt(keyName, (int)value);
            }else if (fieldType == typeof(float))
            {
                Debug.Log(keyName);
                PlayerPrefs.SetFloat(keyName, (float)value);
            }else if (fieldType == typeof(string))
            {
                Debug.Log(keyName);
                PlayerPrefs.SetString(keyName,value.ToString());
            }else if (fieldType == typeof(bool))
            {
                Debug.Log(keyName);
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
            }
        }
        
    }
    public object LoadData(Type type, string keyName)
    {
        return null;
    }
}
