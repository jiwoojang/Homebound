using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEngineLifeIndication : MonoBehaviour
{
    public int maxParticleAmount;

    [SerializeField]
    ParticleSystem enginePS;

    [SerializeField]
    PlayerVanHealth playerHealthScript;

    ParticleSystem.MainModule engineMain;

    private void Start()
    {
        engineMain = enginePS.main;
    }

    float ReturnHealthRatio(float currenthealth, float maxhealth)
    {
        return currenthealth / maxhealth;
    }

    void Update()
    {
        Debug.Log(playerHealthScript._currentHealth);
        Debug.Log(playerHealthScript._maxHealth);
        Debug.Log(engineMain.maxParticles);
        engineMain.maxParticles = (int) (maxParticleAmount * ReturnHealthRatio(playerHealthScript._currentHealth, playerHealthScript._maxHealth));
    }
}
