using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Loot : ScriptableObject
{
    public GameObject lootPrefab;
    public string lootName;
    public int lootChance;
    public Sprite lootSprite;

    public Loot(string lootName, int lootChance, GameObject lootPrefab)
    {
        this.lootName = lootName;
        this.lootChance = lootChance;   
        this.lootPrefab = lootPrefab;
    }
}
