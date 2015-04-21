using UnityEngine;

namespace Assets.Scripts.Camera {
	public class ExtendedFlycam : MonoBehaviour {
		public Transform Transform;

		public float cameraSensitivity = 90;
		public float climbSpeed = 4;
		public float normalMoveSpeed = 10;
		public float slowMoveFactor = 0.25f;
		public float fastMoveFactor = 3;

		private float rotationX = 0.0f;
		private float rotationY = 0.0f;

		void Start() {
			Cursor.visible = true;
		}

		void FixedUpdate() {
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			Transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			Transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

			if (Input.GetKey(KeyCode.AltGr) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
				Transform.position += Transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				Transform.position += Transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			} else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
				Transform.position += Transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
				Transform.position += Transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
			} else {
				Transform.position += Transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
				Transform.position += Transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
			}


			if (Input.GetKey(KeyCode.Space)) { Transform.position += Transform.up * climbSpeed * Time.deltaTime; }
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { Transform.position -= Transform.up * climbSpeed * Time.deltaTime; }

			if (Input.GetKeyDown(KeyCode.End)) {
				Cursor.visible = Cursor.visible == false;
			}
		}
	}
}