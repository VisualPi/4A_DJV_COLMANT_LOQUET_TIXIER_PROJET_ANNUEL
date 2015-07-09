using System.Collections.Generic;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Manager {
    public abstract class ManagedScrollList<T> : MonoBehaviour {
        [SerializeField]
        protected List<T> ItemList;
        [SerializeField]
        protected Transform ContentPanel;

        protected virtual void ClearPanel() {
            Log.Debug.Ui("ClearPanel() : {0}", ContentPanel.name);
            // For each child in ContentPanel...
            for (var i = 0; i < ContentPanel.transform.childCount; i++) {
                // We destroy it.
                Destroy(ContentPanel.transform.GetChild(i).gameObject);
            }
        }

        public void RemoveAll() {
            Log.Debug.Ui("RemoveAll() : {0}", ItemList);
            ItemList.Clear();
            ClearPanel();
        }

        public bool RemoveItem(T items) {
            if (!ItemList.Remove(items))
                return false;

            ClearPanel();
            _populateList();
            return true;
        }

        protected abstract void _populateList();

        public void PopulatePanel(IEnumerable<T> items) {
            Log.Debug.Ui("PopulatePanel(items) : {0}", ItemList);
            ItemList.AddRange(items);
            ClearPanel();
            _populateList();
        }
    }
}
