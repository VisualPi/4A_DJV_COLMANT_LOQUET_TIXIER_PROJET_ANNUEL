using UnityEngine;

namespace Assets.Scripts.GUI {
	public class RunGame : MonoBehaviour {
		public void OnJoinGame(string loadLevel) {
			Application.LoadLevel(loadLevel);
		}
	}
}
