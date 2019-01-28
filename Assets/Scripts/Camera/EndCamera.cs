using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebound.Player;

public class EndCamera : MonoBehaviour
{

    Camera thisCamera;

    [SerializeField]
    Transform target;

    [SerializeField]
    TerrainGeneration terrainGenerationScript;

    [SerializeField]
    PostCardOverlay postCardScript;

    [SerializeField]
    GameObject awming;

    [SerializeField]
    DayNightCycle dayNightCycleScript;

    [SerializeField]
    ParticleSystem enginePS;

    [SerializeField]
    AudioSource engineAudio;

    [SerializeField]
    PlayerVanHealth playerHealthScript;

    [SerializeField]
    ScorePerSecond scoreScript;

    [SerializeField]
    ObstacleGenerator obstacleGen;

    [SerializeField]
    public AudioSource puttering;

    float rotateYSpeed = 5.0f;
    float currentRotationYVel;

    float dstFromTarget = 50.0f;
    float currentDstFromTargetVel;

    public bool gameover;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover)
        {
            dstFromTarget = Mathf.SmoothDamp(dstFromTarget, 4.0f, ref currentDstFromTargetVel, 20.0f * Time.deltaTime, 50.0f);
            transform.position = target.position - transform.forward * dstFromTarget ;

            rotateYSpeed = Mathf.SmoothDamp(rotateYSpeed, 0.0f, ref currentRotationYVel, 20.0f * Time.deltaTime, 1.20f);
            transform.RotateAround(target.transform.position, Vector3.up, rotateYSpeed);
        }
    }

    public void GameOverSequenceMethod()
    {
        if (!gameover)
        {
            StartCoroutine(GameOverSequence());
        }
    }

    IEnumerator GameOverSequence()
    {
        gameover = true;
        obstacleGen.StopObstacleGeneration();

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().stopVibration = true;

        foreach (TerrainMovement t in GameObject.FindObjectsOfType<TerrainMovement>())
        {
            t.speed = 0.0f;
        }

        engineAudio.Stop();

        while (currentRotationYVel > 0)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        foreach (GameObject turf in terrainGenerationScript.terrainCopies)
        {
            turf.GetComponent<TerrainMovement>().gameover = true;
        }
        dayNightCycleScript.StopDayNightCycle();

        ParticleSystem.EmissionModule enginePSEmission = enginePS.emission;
        enginePSEmission.rateOverTime = 0;

        yield return new WaitForSeconds(3.0f);
        postCardScript.BringInPostCard();

        yield return new WaitForSeconds(2.0f);
        scoreScript.DisplayScore();
        awming.SetActive(true);

    }
}
