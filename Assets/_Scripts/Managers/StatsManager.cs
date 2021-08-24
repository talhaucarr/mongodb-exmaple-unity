using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;


[CreateAssetMenu(menuName = "ScriptableObjects/Singeltons/StatsManager")]
public class StatsManager : ScriptableSingleton<StatsManager>
{
    [Header("Informations")]
    [SerializeField] [ShowOnly] private string username;
    [SerializeField] [ShowOnly] private string characterClass;
    [SerializeField] [ShowOnly] private int level;
    [SerializeField] [ShowOnly] private int exp;


    [Header("Inventory")]
    [SerializeField] [ShowOnly] private int gold;


    [Header("Stats")]
    [SerializeField] [ShowOnly] private int statPoints;
    [SerializeField] [ShowOnly] private int hp;
    [SerializeField] [ShowOnly] private int str;
    [SerializeField] [ShowOnly] private int dex;
    [SerializeField] [ShowOnly] private int vitality;

    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    public int StatPoints
    {
        get { return statPoints; }
        set { statPoints = value; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int STR
    {
        get { return str; }
        set { str = value; }
    }
    public int DEX
    {
        get { return dex; }
        set { dex = value; }
    }
    public int VIT
    {
        get { return vitality; }
        set { vitality = value; }
    }
}
