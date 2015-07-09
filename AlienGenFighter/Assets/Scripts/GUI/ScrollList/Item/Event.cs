using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Item {
    [System.Serializable]
    public class ItemEvent {
        public Sprite Icon;
        public string Title;
        public string Description;
        public string Timestamp;

        public DoWorkFunc DoWorkEvent;
    }
}