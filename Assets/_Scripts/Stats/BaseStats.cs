using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] private int startingLevel = 1;
        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;

        public float GetStat(Stat stats)
        {
            
            return progression.GetStat(stats,characterClass, startingLevel);
        }
    }
}


