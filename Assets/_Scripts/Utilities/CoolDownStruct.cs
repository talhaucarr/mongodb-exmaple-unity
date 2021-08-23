using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Utilities
{
    [Serializable]
    public struct Cooldown
    {
        public float cooldown;
        private float timer;

        public bool IsReady
        {
            get { if (timer >= cooldown) { return true; } else { return false; } }
        }

        public Cooldown(float _cooldown)
        {
            cooldown = _cooldown;
            timer = _cooldown;
        }

        public void Step(float deltaTime)
        {
            if (timer < cooldown)
            {
                timer += deltaTime;
            }
        }

        public void EnterCooldown()
        {
            timer = 0;
        }
    }
}
