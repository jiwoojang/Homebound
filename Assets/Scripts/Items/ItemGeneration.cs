using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGeneration : MonoBehaviour
{
    public GameObject[] itemsToBeGenerated;

    public GameObject GenerateRandomItem(Vector3 itemSpawnPos)
    {
        Vector2 itemSpawnBoundaries = new Vector2(-14, 14);
        int randomIndex = Random.Range(0, itemsToBeGenerated.Length);
        Vector3 sideShiftFactor = new Vector3(Random.Range(itemSpawnBoundaries.x, itemSpawnBoundaries.y), 1, 0);
        return Instantiate(itemsToBeGenerated[randomIndex], itemSpawnPos + sideShiftFactor, Quaternion.identity);
    }
}
