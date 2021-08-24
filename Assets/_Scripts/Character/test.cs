using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    [SerializeField] private TextMesh nicknameText;
    private void Update()
    {
        StatsManager.Instance.Username = "talla";
        StatsManager.Instance.Level = 15;
        StatsManager.Instance.Exp = 20;
        nicknameText.text = $"Nick:{StatsManager.Instance.Username}";
    }
}
