using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager
{
    public static void SaveStar(int level, int star)
    {
        string field = "level" + level;
        PlayerPrefs.SetInt(field, star);
    }
    public static int LoadStar(int level)
    {
        string field = "level" + level;
        return PlayerPrefs.GetInt(field);
    }
    public static void SaveSuccess(int level)
    {
        string field = "isSuccess" + level;
        PlayerPrefs.SetInt(field, 1);
    }
    public static int NextLevel()
    {
        int i = 0;
        string field = "isSuccess";
        while (true) 
        {
            i++;
            if (!PlayerPrefs.HasKey(field + i)) return i;
        }
    }
    public static void SaveHighTimes(int level, int time)
    {
        string field = "highTimes" + level;
        PlayerPrefs.SetInt(field, time);
    }
    public static int LoadHighTimes(int level)
    {
        string field = "highTimes" + level;
        return PlayerPrefs.GetInt(field);
    }
}