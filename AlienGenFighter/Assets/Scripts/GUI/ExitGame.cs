using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class ExitGame : MonoBehaviour
    {
        [SerializeField]
        private Button _buttonExitGame;

        void Awake()
        {
            if ( Application.isWebPlayer || Application.isEditor )
                _buttonExitGame.interactable = false;
        }

        public void OnExitGame()
        {
            Application.Quit();
        }
    }
}
