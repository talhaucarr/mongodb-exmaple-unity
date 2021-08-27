using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModule
{
    bool CanAttack(GameObject enemy);
    void Attack(GameObject enemy);
}
