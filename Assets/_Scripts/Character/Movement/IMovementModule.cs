using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModule
{

    void StartMoveAction(Vector3 destination);
    void Stop();
}
