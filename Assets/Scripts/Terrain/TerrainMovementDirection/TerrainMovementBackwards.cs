using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMovementBackwards : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TerrainBlock"))
        {
            other.GetComponent<TerrainMovement>().ChangeDirection(TerrainMovement.TerrainDirection.Backward);
        }
    }
}
