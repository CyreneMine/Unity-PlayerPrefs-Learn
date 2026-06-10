using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
    public string name;
    public int age;
    public float height;
    //true是男
    public bool sex;
    public List<int> list = new List<int>() ;
    public Dictionary<int, string> dic = new Dictionary<int, string>();
    public ItemInfo itemInfo = new ItemInfo();
    public List<ItemInfo> itemList = new List<ItemInfo>();
    public Dictionary<int, ItemInfo> itemDic = new Dictionary<int, ItemInfo>();

    public void Show()
    {
        Debug.Log("Show:");
        Debug.Log($"name:{name}");
        Debug.Log($"age:{age}");
        Debug.Log($"height:{height}");
        Debug.Log($"sex:{sex}");

        if (list != null)
        {
            foreach (int i in list)
            {
                Debug.Log($"list:{i}");
            }
        }

        if (dic != null)
        {
            foreach (var kv in dic)
            {
                Debug.Log($"dic key:{kv.Key}, value:{kv.Value}");
            }
        }

        if (itemInfo != null)
        {
            Debug.Log($"itemInfo num:{itemInfo.num}, str:{itemInfo.itemName}");
        }
        else
        {
            Debug.Log("itemInfo:null");
        }

        if (itemList != null)
        {
            foreach (ItemInfo item in itemList)
            {
                if (item != null)
                {
                    Debug.Log($"itemList item num:{item.num}, str:{item.itemName}");
                }
            }
        }

        if (itemDic != null)
        {
            foreach (var kv in itemDic)
            {
                if (kv.Value != null)
                {
                    Debug.Log($"itemDic key:{kv.Key}, num:{kv.Value.num}, str:{kv.Value.itemName}");
                }
            }
        }
    }
}

public class ItemInfo
{
    public int num;
    public string itemName;
    public ItemInfo()
    {
        
    }
    public  ItemInfo(int num, string itemName)
    {
        this.num = num;
        this.itemName = itemName;
    }
}
public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInfo player = new PlayerInfo();
        /*
        第一次运行的时候模拟玩家游玩后保存退出 
        player.name = "Cyrene";
        player.age = 20;
        player.height = 9999;
        player.sex = false;
        player.list = new List<int>(){13,13,1};
        player.dic = new Dictionary<int, string>()
        {
            {1,"ljw"},
            {2,"cyrene"},
            {3,"夫妻"},
        };
        player.itemInfo = new ItemInfo(4000,"书页");
        PlayerPrefsDataMgr.Instance.SaveData(player,"Cyrene");
        */
        //下面是检查上一次运行是否正常保存
        player = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerInfo), "Cyrene") as  PlayerInfo;
        player.Show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

