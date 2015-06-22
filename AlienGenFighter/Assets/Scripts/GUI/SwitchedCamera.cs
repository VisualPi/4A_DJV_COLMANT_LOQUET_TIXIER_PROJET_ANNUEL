using UnityEngine;

public class SwitchedCamera : MonoBehaviour
{
    [SerializeField]
    private CameraManagerScript _managerScript;

    public void OnSwitchCamera(Camera cam)
    {
        _managerScript.ShowCamera(cam);
    }
}