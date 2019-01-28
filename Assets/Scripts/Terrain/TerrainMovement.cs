using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    private ItemGeneration itemGenerationScript;

    public enum TerrainDirection
    {
        Forward, Backward
    }

    public enum TerrainType
    {
        Road, Mountain
    }

    public bool gameover;

    public TerrainDirection terrainDirection;
    public TerrainType terrainType;

    public float speed;

    private float currentSpeed;

    public float initialCenterOffset;

    private void Awake()
    {
        RandomizeCenterOffset();
        if (terrainType == TerrainType.Road)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);   
    }

    private void Start()
    {
        speed = GameObject.FindGameObjectWithTag("GlobalSpeed").GetComponent<GlobalSpeed>().speed;
        itemGenerationScript = GameObject.FindGameObjectWithTag("TerrainGenerator").GetComponent<ItemGeneration>();
    }

    private void RandomizeCenterOffset()
    {
        initialCenterOffset = Random.Range(-17.0f, 17.0f);
        if (initialCenterOffset > -8.0f && initialCenterOffset < 8.0f)
            RandomizeCenterOffset();
    }

    public void ChangeDirection(TerrainDirection direction)
    {
        this.terrainDirection = direction;
    }

    void Update()
    {
        if (transform.position.z < -60.0f)
        {
            if (terrainType == TerrainType.Road)
            {
                Vector3 spawnPosition = new Vector3(0, transform.position.y, transform.position.z +
                    GameObject.FindGameObjectWithTag("TerrainGenerator").GetComponent<TerrainGeneration>().shiftMaxAmount);
                transform.position = spawnPosition;
                if (itemGenerationScript != null)
                {
                    Debug.Log("Item Script Not Found");
                    itemGenerationScript.GenerateRandomItem(spawnPosition).transform.SetParent(transform);
                }
            }
            else
            {
                RandomizeCenterOffset();
                transform.position = new Vector3(initialCenterOffset, transform.position.y, transform.position.z +
                    GameObject.FindGameObjectWithTag("TerrainGenerator").GetComponent<TerrainGeneration>().shiftMaxAmount);
            }
        }
        if (terrainDirection == TerrainDirection.Forward)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
        if (terrainDirection == TerrainDirection.Backward)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        if (gameover)
        {
            speed = Mathf.SmoothDamp(speed, 0.0f, ref currentSpeed, 3.0f, 15.0f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DestroyBoundary"))
        {
            Destroy(gameObject);
        }
    }
}
