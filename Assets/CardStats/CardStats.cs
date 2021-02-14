using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardStats", menuName = "ScriptableObjects/CardStats", order = 1)]
public class CardStats : ScriptableObject
{
    public string title;
    public string description;
    public Sprite image;

    public bool canBeDenied = true;
    
    public float lumpMoney;             // 0 to 1000? Rent is 100 every 4 turns?
    public int lumpCredit;

    // public float passiveMoney;
    // public int passiveCredit;

    public float happiness;
    public int energyCost;

    public float noLumpMoney;
    public int noLumpCredit;

    // public float noPassiveMoney;
    // public int noPassiveCredit;

    public float noHappiness;
    public int noEnergyCost;
}
