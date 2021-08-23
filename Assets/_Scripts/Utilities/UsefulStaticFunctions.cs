using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class UsefulStaticFunctions
    {
        public static Transform GetClosestTransform(Vector2 position,Transform[] transforms)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;     
            foreach (Transform t in transforms)
            {
                float dist = Vector2.Distance(t.position, position);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }

        public static Transform GetClosestTransform(Vector2 position, List<Transform> transforms)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            foreach (Transform t in transforms)
            {
                float dist = Vector2.Distance(t.position, position);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }

        public static Transform GetFarthestEnemy(Vector2 position, List<Transform> enemies)
        {
            Transform tMin = null;
            float minDist = -1*Mathf.Infinity;
            foreach (Transform t in enemies)
            {
                float dist = Vector2.Distance(t.position, position);
                if (dist > minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }

        public static Transform GetClosestTransform(Vector2 position, Collider2D[] transforms)
        {
            Transform tMin = null;
            float minDist = Mathf.Infinity;
            foreach (Collider2D t in transforms)
            {
                float dist = Vector2.Distance(t.transform.position, position);
                if (dist < minDist)
                {
                    tMin = t.transform;
                    minDist = dist;
                }
            }
            return tMin;
        }

        public static RaycastHit2D[] Raycast2DWithIgnore(Vector2 position , Vector2 dir, float range, GameObject ignoredObject)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(position, dir, range);

            List<RaycastHit2D> resultHits = new List<RaycastHit2D>();

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject != ignoredObject)
                {
                    resultHits.Add(hit);
                }
            }

            return resultHits.ToArray();
        }
    }
}
