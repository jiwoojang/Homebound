using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebound.Player;

public class ObstacleGenerator : MonoBehaviour
{
    [SerializeField]
    private List<ObstaclePackage> obstaclePackages;

    [SerializeField]
    private bool _generateObjects;

    [System.Serializable]
    public class ObstaclePackage 
    {
        public GameObject[] obstacleObjects;
        public PlayBounds spawnBounds;
        public Vector2 generationDelay;
    }

    private void Awake() 
    {
        _generateObjects = true;

        foreach(ObstaclePackage package in obstaclePackages)
        {
            StartCoroutine(GenerateObjects(package));
        }
    }

    public void StopObstacleGeneration() 
    {
        _generateObjects = false;
    }

    IEnumerator GenerateObjects(ObstaclePackage obstaclePackage) 
    {
        if (obstaclePackage.obstacleObjects.Length > 0)
        {
            while (_generateObjects) 
            {
                // Randomly pick an object
                GameObject objectToGenerate = obstaclePackage.obstacleObjects[Random.Range(0, obstaclePackage.obstacleObjects.Length - 1)];

                // Randomly pick an x coordinate for that object
                float generatedX = Random.Range(obstaclePackage.spawnBounds.GetLeftBound().x, obstaclePackage.spawnBounds.GetRightBound().x);

                // Instantiate the thing
                Instantiate(objectToGenerate, new Vector3(generatedX, obstaclePackage.spawnBounds.GetLeftBound().y, obstaclePackage.spawnBounds.GetLeftBound().z), Quaternion.identity);

                yield return new WaitForSeconds(Random.Range(obstaclePackage.generationDelay.x, obstaclePackage.generationDelay.y));
            }
        }
    }
}
