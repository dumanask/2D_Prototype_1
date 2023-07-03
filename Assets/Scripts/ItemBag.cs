using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    Item GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Item> possibleItems = new List<Item>();

        foreach (Item item in itemList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if(possibleItems.Count > 0)
        {
            Item droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }

    public void InstatiateItem(Vector3 spawnPosition)
    {
        Item droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject itemGameObject = Instantiate(droppedItem.itemPrefab, spawnPosition, Quaternion.identity);
            itemGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.itemSprite;

            float dropForce = 300f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
            itemGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }
    }
}
