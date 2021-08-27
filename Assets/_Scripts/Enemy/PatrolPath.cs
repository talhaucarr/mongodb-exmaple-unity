using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Scripts.Enemy
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WaypointGizmoRadius = .3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.blue;
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPoint(i), WaypointGizmoRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
                return 0;
            return i + 1;
        }

        public Vector3 GetWayPoint(int i)
        {
            return  transform.GetChild(i).position;
        }
    }
}

