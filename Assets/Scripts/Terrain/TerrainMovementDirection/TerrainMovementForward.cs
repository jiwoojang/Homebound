using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovementForward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TerrainBlock"))
        {
            other.GetComponent<TerrainMovement>().ChangeDirection(TerrainMovement.TerrainDirection.Forward);
        }
    }
}
