using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int num;
}
public class Player
{
    public string name;
    public int age;
    public int atk;
    public int def;
    public List<Item> items = new List<Item>();
    private string keyName;
    public void Save()
    {
        PlayerPrefs.SetString(keyName+"_name", name);
        PlayerPrefs.SetInt(keyName+"_age", age);
        PlayerPrefs.SetInt(keyName+"_atk", atk);
        PlayerPrefs.SetInt(keyName+"_def", def);
        PlayerPrefs.SetInt(keyName+"_ItemCount", items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetInt(keyName+"_itemId"+i, items[i].id);
            PlayerPrefs.SetInt(keyName+"_itemNum"+i, items[i].num);
        }
        PlayerPrefs.Save();
    }

    public void Load(string keyName)
    {
        items.Clear();
        this.keyName = keyName;
        name = PlayerPrefs.GetString(keyName+"_name","Cyrene");
        age = PlayerPrefs.GetInt(keyName+"_age",20);
        atk = PlayerPrefs.GetInt(keyName+"_atk",99999);
        def = PlayerPrefs.GetInt(keyName+"_def",99999);
        
        for (int i = 0; i < PlayerPrefs.GetInt(keyName+"_ItemCount"); i++)
        {
            Item item = new Item();
            item.id = PlayerPrefs.GetInt(keyName+"_itemId"+i);
            item.num = PlayerPrefs.GetInt(keyName+"_itemNum"+i);
            items.Add(item);
        }
        
    }

    public void Show()
    {
        Debug.Log(name);
        Debug.Log(age);
        Debug.Log(atk);
        Debug.Log(def);

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"物品id:{items[i].id} 有{items[i].num}个");
        }
        Debug.Log($"一共有{items.Count}个道具");
    }
}
public class Lesson1_PlayerPrefs : MonoBehaviour
{
    
    void Start()
    {
        Player p = new Player();
        p.Load("Player1");
        p.Save();
        p.Show();
        
        Player p2 = new Player();
        p2.Load("Player2");
        // p2.name = "CyreneMine";
        p2.Save();
        p2.Show();
        
        // p.name = "CyreneMine";
        // p.age = 22;
        // p.Save();
        //
        //
        // Item item = new Item();
        // item.id = 1;
        // item.num = 1;
        // p.items.Add(item);
        // item = new Item();
        // item.id = 2;
        // item.num = 2;
        // p.items.Add(item);
        //
        // p.Show();
        
        // PlayerPrefs.DeleteAll();
    }
    
    void Update()
    {
        
    }
}
