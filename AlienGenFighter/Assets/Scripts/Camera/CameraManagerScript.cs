using UnityEngine;

public class CameraManagerScript : MonoBehaviour {
    public Camera CurrentCamera;

    public void Start() {
        ShowCamera(CurrentCamera);
    }

    public void ShowCamera(Camera cam) {
        HideCurrentCamera();
        (CurrentCamera = cam).enabled = true;
    }

    public void HideCurrentCamera() {
        if (CurrentCamera != null)
            CurrentCamera.enabled = false;

        CurrentCamera = null;
    }
}
