using UnityEngine;

public class CameraManagerScript : MonoBehaviour {
    [SerializeField] private Camera _currentCamera;

    public void Start() {
        _currentCamera.enabled = true;
    }

    public void ShowCamera(Camera cam) {
        if (IsCurrentCamera(cam)) return;

        HideCurrentCamera();
        (_currentCamera = cam).enabled = true;
    }

    public void HideCurrentCamera() {
        if (_currentCamera != null)
            _currentCamera.enabled = false;

        _currentCamera = null;
    }

    public bool IsCurrentCamera(Camera cam) {
        return _currentCamera == cam;
    }
}
