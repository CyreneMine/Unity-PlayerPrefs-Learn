using System;
using System.Collections.Generic;
using UnityEngine;


public class RankListInfo
{
    public List<RankInfo> ranks;

    public void updateRank(RankInfo newRank)
    {
        bool flag = true;
        for (int i = 0; i < ranks.Count; i++)
        {
            if (ranks[i].playerName == newRank.playerName)
            {
                flag = false;
                if (ranks[i].playerScore < newRank.playerScore)
                {
                    ranks[i].playerScore = newRank.playerScore;
                    ranks[i].playerTime = newRank.playerTime;
                    
                }
                else if (ranks[i].playerTime > newRank.playerTime && ranks[i].playerScore == newRank.playerScore)
                {
                    ranks[i].playerTime = newRank.playerTime;
                }
                break;
            }
        }

        if (flag)
        {
            ranks.Add(newRank);
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("rankListNum", ranks.Count);
        for (int i = 0; i < ranks.Count; i++)
        {
            PlayerPrefs.SetString("rankInfo"+i, ranks[i].playerName);
            PlayerPrefs.SetInt("rankScore"+i,ranks[i].playerScore);
            PlayerPrefs.SetInt("rankTime"+i,ranks[i].playerTime);
        }
        PlayerPrefs.Save();
    }

    public void Load()
    {
        ranks = new List<RankInfo>();
        int rankCount = PlayerPrefs.GetInt("rankListNum");
        for (int i = 0; i < rankCount; i++)
        {
            string str = PlayerPrefs.GetString("rankInfo"+i, "");
            int score = PlayerPrefs.GetInt("rankScore"+i, 0);
            int time = PlayerPrefs.GetInt("rankTime"+i, 0);
            RankInfo temp = new RankInfo(str,score,time);
            ranks.Add(temp);
        }
    }
    public  void Show()
    {
        
        ranks.Sort((a, b) =>
        {
            /*b.playerScore.CompareTo(a.playerScore)
            大于0  => 左边更大
            等于0  => 一样大
            小于0  => 左边更小*/
            int result = b.playerScore.CompareTo(a.playerScore);
            if (result!=0)
                return result;
            return a.playerTime.CompareTo(b.playerTime);
            
        });
        for (int i = 0; i < ranks.Count; i++)
        {
            Debug.Log($"第{i+1}名:{ranks[i].playerName}\t分数:{ranks[i].playerScore}\t通关时间:{ranks[i].playerTime}");
        }
    }
}

public class RankInfo
{
    public string playerName;
    public int playerScore;
    public int playerTime;
   public RankInfo(string playerName, int playerScore, int playerTime)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
        this.playerTime = playerTime;
    }
}
public class Lesson2 : MonoBehaviour
{
    RankListInfo rankListInfo = new RankListInfo();
    private void Awake()
    {
        rankListInfo.Load();
        RankInfo r1 = new RankInfo("TestDate", 10, 50);
        RankInfo r2 = new RankInfo("TestDate2", 2, 52);
        rankListInfo.updateRank(r1);
        rankListInfo.updateRank(r2);
        rankListInfo.Save();
        
        rankListInfo.Show();
    }

    
}
