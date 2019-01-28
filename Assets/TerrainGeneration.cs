using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    private int shiftForwardAmount = 16;
    public int shiftMaxAmount;

    [Range(1, 20)]
    public int amountToGenerate;

    [SerializeField]
    Transform terrainSpawn;

    [SerializeField]
    GameObject terrainPrefab;

    public GameObject[] terrainCopies;

    private void Start()
    {
        terrainCopies = new GameObject[amountToGenerate];

        int totalShiftDistance = 0;
        for (int i = 0; i < amountToGenerate; i++)
        {
            totalShiftDistance = shiftForwardAmount*i;
            Vector3 terrainSpawnPos = new Vector3(terrainSpawn.position.x, terrainSpawn.position.y
                , terrainSpawn.position.z + totalShiftDistance);
            GameObject terrainCopy = Instantiate(terrainPrefab, terrainSpawnPos, Quaternion.identity);
            terrainCopy.name = "TurfCopy";
            terrainCopies[i] = terrainCopy;

            if (i == amountToGenerate - 1)
            {
                shiftMaxAmount = i * shiftForwardAmount;
            }
        }
    }
}
