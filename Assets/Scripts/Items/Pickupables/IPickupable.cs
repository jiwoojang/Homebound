using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable
{
    void PickupEffect();
    void DropObjectOnPlayer(GameObject playerObject);
}
