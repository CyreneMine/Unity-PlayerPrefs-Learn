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
    public List<Item> items;
    
    public void Save()
    {
        PlayerPrefs.SetString("name", name);
        PlayerPrefs.SetInt("age", age);
        PlayerPrefs.SetInt("atk", atk);
        PlayerPrefs.SetInt("def", def);
        
        PlayerPrefs.SetInt("ItemCount", items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetInt("itemId"+i, items[i].id);
            PlayerPrefs.SetInt("itemNum"+i, items[i].num);
        }
        PlayerPrefs.Save();
    }

    public void Load()
    {
        name = PlayerPrefs.GetString("name","Cyrene");
        age = PlayerPrefs.GetInt("age",20);
        atk = PlayerPrefs.GetInt("atk",99999);
        def = PlayerPrefs.GetInt("def",99999);

        items = new List<Item>();
        
        for (int i = 0; i < PlayerPrefs.GetInt("ItemCount"); i++)
        {
            Item item = new Item();
            item.id = PlayerPrefs.GetInt("itemId"+i);
            item.num = PlayerPrefs.GetInt("itemNum"+i);
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
        p.Load();
        p.Show();
        p.name = "CyreneMine";
        p.age = 22;
        p.Save();
        
        
        Item item = new Item();
        item.id = 1;
        item.num = 1;
        p.items.Add(item);
        item = new Item();
        item.id = 2;
        item.num = 2;
        p.items.Add(item);
        
        p.Show();
        p.Save();
        // PlayerPrefs.DeleteAll();
    }
    
    void Update()
    {
        
    }
}
