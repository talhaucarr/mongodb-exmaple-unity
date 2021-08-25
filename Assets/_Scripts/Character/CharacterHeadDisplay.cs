using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace _Scripts.Character
{
    public class CharacterHeadDisplay : MonoBehaviour
    {

        [SerializeField] private TextMesh usernameText;


        void Start()
        {
            usernameText.text = StatsManager.Instance.Username;
        }

    }

}
