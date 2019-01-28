using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAndRotate : MonoBehaviour
{
    [SerializeField]
    private float _bumpFrequency = 30.0f;

    [SerializeField]
    private float _bumpMagnitude = 1.0f;

    [SerializeField]
    private float _pivotRotationSpeed = 60f;

    private float _avgLocalYPosition;

    // Start is called before the first frame update
    void Start()
    {
        _avgLocalYPosition = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float yPosition = _bumpMagnitude * (Mathf.Sin(_bumpFrequency * Time.time) + 1);
        transform.localPosition = new Vector3(transform.localPosition.x, _avgLocalYPosition + yPosition, transform.localPosition.z);

        transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - _pivotRotationSpeed * Time.deltaTime, transform.localRotation.eulerAngles.z));
    }
}
