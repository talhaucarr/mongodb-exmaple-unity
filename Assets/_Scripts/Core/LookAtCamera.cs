using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Vector3Int rotationOffset;
    private void Update()
    {
        
        transform.LookAt(CameraManager.Instance.MainCamera.transform);
        transform.Rotate(rotationOffset);

    }
}
