using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tags
{
    None,
    Box,
    Obstacle,
    Player,
    Enemy,
    Ground,
}

public class Tag : MonoBehaviour
{
    [SerializeField] List<Tags> tags;
    public List<Tags> Tags => tags;
}