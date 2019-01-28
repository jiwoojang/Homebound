using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlightCycle : MonoBehaviour
{

    [SerializeField]
    private DayNightCycle _cycle;

    [SerializeField]
    Light spotLight1, spotLight2;

    [SerializeField]
    float lightSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Day
        if (_cycle.cycleProgress < 0.5f)
        {
            spotLight1.intensity = Mathf.Lerp(spotLight1.intensity, 1.0f, lightSpeed * Time.deltaTime);
            spotLight2.intensity = Mathf.Lerp(spotLight2.intensity, 1.0f, lightSpeed * Time.deltaTime);
        } 
        else 
        {
            spotLight1.intensity = Mathf.Lerp(spotLight1.intensity, 3.0f, lightSpeed * Time.deltaTime);
            spotLight2.intensity = Mathf.Lerp(spotLight2.intensity, 3.0f, lightSpeed * Time.deltaTime);
        }
    }
}
