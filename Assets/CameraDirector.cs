using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraDirector : MonoBehaviour {
    private Dictionary<string, CinemachineVirtualCamera> _cameras;

    private void Awake() {
        _cameras = new Dictionary<string, CinemachineVirtualCamera>();
        var cameras = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (var camera in cameras) {
            _cameras.Add(camera.gameObject.name, camera);
        }
        
        SwapToCamera("Start");
    }

    public void SwapToCamera(string cameraName) {
        foreach (var camera in _cameras) {
            camera.Value.Priority = 0;
        }
        
        _cameras[cameraName].Priority = 1;
    }
    
}
