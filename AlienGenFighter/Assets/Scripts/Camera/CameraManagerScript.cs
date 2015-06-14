using UnityEngine;

public class CameraManagerScript : MonoBehaviour {

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float _normalMoveSpeed = 40;
    [SerializeField]
    private float _slowMoveFactor = 0.25f;
    [SerializeField]
    private float _fastMoveFactor = 5;

    // Use this for initialization
    void Start() {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        var moveSpeed = _normalMoveSpeed;

        if (Input.GetKey(KeyCode.AltGr) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
            moveSpeed = _normalMoveSpeed * _fastMoveFactor * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            moveSpeed = _normalMoveSpeed * _slowMoveFactor * Time.deltaTime;
        } else {
            moveSpeed *= Time.deltaTime;
        }

        _transform.transform.position += Vector3.forward * moveSpeed * Input.GetAxis("Vertical");
        _transform.transform.position += Vector3.right * moveSpeed * Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space)) {
            _transform.transform.position += Vector3.up * moveSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            _transform.transform.position -= Vector3.up * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.End)) {
            Cursor.visible = Cursor.visible == false;
        }
    }
}
