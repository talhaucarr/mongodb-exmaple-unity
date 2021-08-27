using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementModule
{

    void StartMoveAction(Vector3 destination, float speedFraction);

    void Move(Vector3 destination, float speedFraction);
    void Cancel();
}
