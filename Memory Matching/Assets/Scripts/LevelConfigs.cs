using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelConfigs", menuName ="Level/LevelConfigs")]
public class LevelConfigs : ScriptableObject
{
    public List<LevelConfig> all_level_configs;
}

[System.Serializable]
public class LevelConfig
{
    [Header("Level")]
    public int level;   
    
    [Header("Row of Board")]
    [Range(2,8)] public int row;

    [Header("Column of Board")]
    [Range(2,8)] public int col;

    [Header("Time limit")]
    [Range(30,600)] public float time_limit;

    [Header("2 Star Time")]
    [Range(0, 600)] public float time_2;

    [Header("1 Star Time")]
    [Range(0, 600)] public float time_1;

    [Header("0 Star Time")]
    [Range(0, 600)] public float time_0;

    [Header("Number of Move")]
    public int number_of_moves;

    [Header("Number of Move 2 star")]
    public int number_of_moves_2;

    [Header("Number of Move 1 star")]
    public int number_of_moves_1;

    [Header("Number of Move 0 star")]
    public int number_of_moves_0;
}
