using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBag : MonoBehaviour
{
    
    public List<Loot> LootList = new List<Loot>();
    public GameObject lootPrefab;



    Loot getDroppedLoot()
    {
        int randomNumber = Random.Range(1, 101); //1-100
        List<Loot> possibleLootList = new List<Loot>();

        foreach (Loot loot in LootList)
        {
            if (randomNumber <= loot.lootChance)
            {
                possibleLootList.Add(loot);


            }
        }

        if (possibleLootList.Count > 0)
        {
            Loot droppedLoot = possibleLootList[Random.Range(0, possibleLootList.Count)];
            return droppedLoot;
        }
        Debug.LogWarning("NoDropedItem");
        return null;
    }
    

    public void InstanateLoot(Vector3 dropedPos)
    {
        Loot dropedItem = getDroppedLoot();
        if (dropedItem != null)
        {
            GameObject lootGO = Instantiate(dropedItem.lootPrefab, dropedPos, Quaternion.identity);
            lootGO.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 0f);

        }
    }
}
