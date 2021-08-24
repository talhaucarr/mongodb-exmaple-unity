using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHeadDisplay : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI usernameText;


    void Start()
    {
        usernameText.text = StatsManager.Instance.Username;
    }

}
