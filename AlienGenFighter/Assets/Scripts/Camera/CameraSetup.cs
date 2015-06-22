using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    void Start()
    {
        Debug.Log("size" + GameData.MapSize.x + ", " + GameData.MapSize.z);
        _camera.transform.position = new Vector3(GameData.MapSize.x / 2, 500, GameData.MapSize.z / 2);
        _camera.orthographicSize = GameData.MapSize.x * 0.66f;
    }
}
