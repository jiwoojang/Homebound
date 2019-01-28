
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCan : MonoBehaviour, IPickupable
{

    [SerializeField]
    private float _forceOnCollected = 10.0f;
    public AudioSource gasPickUp;

    [SerializeField]
    PlayerVanHealth vanHealth;

    ScorePerSecond scoreScript;

    GlobalSpeed globalSpeed;

    public float healthAddAmount;

    private void Awake()
    {
        gasPickUp = GetComponent<AudioSource>();

        GameObject player = GameObject.FindWithTag("Player");
        vanHealth = player.GetComponent<PlayerVanHealth>();
    }

    private void Start()
    {
        scoreScript = GameObject.FindGameObjectWithTag("ScoreScriptObj").GetComponent<ScorePerSecond>();
        globalSpeed = globalSpeed = GameObject.FindGameObjectWithTag("GlobalSpeed").GetComponent<GlobalSpeed>();
    }

    public void PickupEffect()
    {
        globalSpeed.speed += 0.1f;
        scoreScript.scoreAmount += 1;
        vanHealth.AddPlayerVanHealth(healthAddAmount);
        StartCoroutine(PickupThenDestroy());
    }

    public void DropObjectOnPlayer(GameObject a){}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupEffect();
        }
    }

    IEnumerator PickupThenDestroy()
    {
        gasPickUp.Play();
        gasPickUp.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        gasPickUp.transform.GetChild(0).GetChild(0).GetComponentInChildren<MeshRenderer>().enabled = false;
        while (gasPickUp.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
