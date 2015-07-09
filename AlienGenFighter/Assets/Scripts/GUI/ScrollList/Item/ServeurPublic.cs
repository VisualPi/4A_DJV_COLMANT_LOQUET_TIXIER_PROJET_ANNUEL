using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Item {
    [System.Serializable]
    public class ItemServeurPublic {
        public int Id;
        public string ServerName;
        public Sprite Icon;
        public string CurrentNbPlayer;
        public string MaxNbPlayer;
        public string Ping;

        public DoWorkFuncId DoWork;
    }
}