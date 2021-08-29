using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class CameraManager : AutoCleanupSingleton<CameraManager>
{
    [SerializeField] private Camera _camera;

    public Camera MainCamera { get => _camera; }
}
