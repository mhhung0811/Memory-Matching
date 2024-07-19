using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "CardConfigs", menuName ="Cards/CardConfigs")]
public class Cards : ScriptableObject
{
    public List<Card> card_configs;
}
[System.Serializable]
public class Card
{   
    public int card_id;
    public Sprite card_image;
}

