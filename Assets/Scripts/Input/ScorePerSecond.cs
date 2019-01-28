using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePerSecond : MonoBehaviour
{

    public float scoreAmount;
    public float scoreIncreasePerSecond;

    [SerializeField]
    Text scoreText;

    GlobalSpeed globalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        globalSpeed = globalSpeed = GameObject.FindGameObjectWithTag("GlobalSpeed").GetComponent<GlobalSpeed>();
        scoreAmount = 0f;
        scoreIncreasePerSecond = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        scoreAmount += scoreIncreasePerSecond * globalSpeed.speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Debug.Log((int)scoreAmount + "Score");
    }

    public void DisplayScore()
    {
        scoreText.enabled = true;
        scoreText.text = "Score: " + (int) scoreAmount;
    }
}
