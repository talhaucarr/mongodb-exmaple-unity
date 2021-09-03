using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace _Scripts.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] private ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.CharacterClass == characterClass)
                    continue;
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat != stat)
                        continue;
                    
                    if(progressionStat.levels.Length < level)
                        continue;
                    
                    return progressionStat.levels[level - 1];
                }
                
            }
            return 0;
        }
        
        [System.Serializable]
        class ProgressionCharacterClass
        {
            [SerializeField] private CharacterClass characterClass;

            public ProgressionStat[] stats;
            

            public CharacterClass CharacterClass
            {
                get => characterClass;
                set => value = characterClass;
            }
            
        }
        
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}

