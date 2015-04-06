using UnityEngine;
using System.Collections;

public class CameraManagerScript : MonoBehaviour {

    [SerializeField]
    Camera CameraManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKey(KeyCode.UpArrow))
        {
            CameraManager.transform.position += Vector3.forward * 10.0f; 
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            CameraManager.transform.position += Vector3.forward * -10.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CameraManager.transform.position += Vector3.right * -10.0f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            CameraManager.transform.position += Vector3.right * 10.0f;
        }
        if (Input.mouseScrollDelta.y < new Vector2().y)
        {
            //Debug.Log(Input.mouseScrollDelta.ToString());
            CameraManager.transform.position += Vector3.up * 10.0f;
        }
        if (Input.mouseScrollDelta.y > new Vector2().y)
        {
           // Debug.Log(Input.mouseScrollDelta.ToString());
            CameraManager.transform.position += Vector3.up * -10.0f;
        }



	}
}
