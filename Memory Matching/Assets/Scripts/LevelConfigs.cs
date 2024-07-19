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

    [Header("Number of Mistakes")]
    public int number_of_mistakes;

}
