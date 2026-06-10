using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
    public string name;
    public int age;
    public float height;
    //true是男
    public bool sex;

    public List<int> list = new List<int>() { 1, 2, 3, 4 };
}
public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.name = "TestName";
        playerInfo.age = 20;
        playerInfo.height = 175.5f;
        playerInfo.sex = true;
        PlayerPrefsDataMgr.Instance.SaveData(playerInfo,"TestPlayer1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
