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
            return ItemList.Remove(items);
        }

        protected abstract void _populateList(List<T> items);

        public void PopulatePanel(IEnumerable<T> items) {
            ItemList.AddRange(items);
            Log.Debug.Ui("PopulatePanel(items) : {0}", ItemList);
            _populateList(items as List<T>);
        }

        public void RefreshPanel() {
            Log.Debug.Ui("RefreshPanel()");
            ClearPanel();
            _populateList(ItemList);
        }
    }
}
