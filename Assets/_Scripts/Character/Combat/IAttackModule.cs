using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackModule
{
    void Attack(GameObject enemy);
    void AttackCancel();
}
