using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item: ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public GameObject itemPrefab;
    public int dropChance;

    public Item(string itemName, int dropChance, GameObject itemPrefab) 
    {
        this.itemName = itemName;
        this.dropChance = dropChance;
        this.itemPrefab = itemPrefab;
    }


}
