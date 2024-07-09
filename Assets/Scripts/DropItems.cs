using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DropItems : MonoBehaviour
{
    /* [Serializable]
     public struct Item
     {
         public GameObject prefab;
         public float spawnRate;
     }*/

    [SerializeField]
    private List<Item> ItemsToDorp = new List<Item>();

    public bool StartBool = false;
    private float targetTime;

    private float screenLeft;
    private float screenRight;

    public void Start()
    {
        targetTime = 2;
        screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }
    private void Update()
    {
        if (StartBool)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0)
            {
                DropRandomItems();
                targetTime = UnityEngine.Random.Range(3, 6);
                //Debug.Log(targetTime);
            }
        }
    }

    public void DropRandomItems()
    {
        float spawnX = UnityEngine.Random.Range(screenLeft, screenRight);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0);

        Instantiate(ItemsToDorp[GetRandomSpawn()].gameObject, spawnPosition, Quaternion.identity);
    }
    public int GetRandomSpawn()
    {
        float random = UnityEngine.Random.Range(0, 1f);
        float numForAdding = 0;
        float total = 0;

        foreach (Item item in ItemsToDorp)
        {
            total += item.spawnRate;
        }

        for (int i = 0; i < ItemsToDorp.Count; i++)
        {
            if (ItemsToDorp[i].spawnRate / total + numForAdding >= random)
            {
                return i;
            }
            else
            {
                numForAdding += ItemsToDorp[i].spawnRate / total;
            }
        }
        return 0;
    }

}
