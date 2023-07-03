using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }
    
}
