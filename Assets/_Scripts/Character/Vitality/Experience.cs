using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Character.Vitality
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float expPoints = 0;

        public void GainExperience(float experience)
        {
            expPoints += experience;
        }
    }
}
