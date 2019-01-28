
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPickupable
{

    [SerializeField]
    private float _forceOnCollected = 10.0f;
    public AudioSource hit;

    [SerializeField]
    PlayerVanHealth vanHealth;

    ScorePerSecond scoreScript;

    GlobalSpeed globalSpeed;

    public float healthAddAmount;

    private void Awake()
    {
        hit = GetComponent<AudioSource>();

        GameObject player = GameObject.FindWithTag("Player");
        vanHealth = player.GetComponent<PlayerVanHealth>();
    }

    private void Start()
    {
        scoreScript = GameObject.FindGameObjectWithTag("ScoreScriptObj").GetComponent<ScorePerSecond>();
        globalSpeed = GameObject.FindGameObjectWithTag("GlobalSpeed").GetComponent<GlobalSpeed>();
    }

    public void PickupEffect()
    {
        if (globalSpeed.speed >= 0.5f)
            globalSpeed.speed -= 0.3f;
        vanHealth.AddPlayerVanHealth(healthAddAmount);
        StartCoroutine(PickupThenDestroy());
    }

    public void DropObjectOnPlayer(GameObject a){}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreScript.scoreAmount -= 3;
            PickupEffect();
        }
    }

    IEnumerator PickupThenDestroy()
    {
        hit.Play();
        if (hit.transform.GetComponent<MeshRenderer>())
            hit.transform.GetComponent<MeshRenderer>().enabled = false;
        while (hit.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
