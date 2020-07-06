using System;
using UnityEngine;

// 스펠의 정보를 담아둠
[Serializable]
public class Spell
{
    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;


    [SerializeField]
    private Color barColor;

    public string MyName
    {
        get
        {
            return name;
        }
    }

    public int MyDamage
    {
        get
        {
            return damage;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public float MySpeed
    {
        get
        {
            return speed;
        }
    }

    public float MyCastTime
    {
        get
        {
            return castTime;
        }

    }

    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }
    }

    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }
}
