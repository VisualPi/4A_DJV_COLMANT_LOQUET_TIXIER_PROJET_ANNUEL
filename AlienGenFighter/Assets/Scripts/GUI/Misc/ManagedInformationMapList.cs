using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GUI.Sample;
using UnityEngine;

namespace Assets.Scripts.GUI.Misc {
    [System.Serializable]
    public class ItemInformation {
        public string Name;
        public string Value;
    }

    public class ManagedInformationMapList : MonoBehaviour {
        public GameObject SampleInformationItem;
        public List<ItemInformation> ItemList;
        public Transform ContentPanel;

        /// <summary><c>ClearList</c> delete all data in <c>ContentPanel</c>.</summary>
        public void ClearList() {
            ItemList.Clear();
            // For each child in ContentPanel...
            for (var i = 0; i < ContentPanel.transform.childCount; i++) {
                // We destroy it.
                Destroy(ContentPanel.transform.GetChild(i).gameObject);
            }
        }

        /// <summary><c>PopulateList</c> add data from <c>ItemList</c> into <c>ContentPanel</c>.</summary>
        public void PopulateList() {

            // For each item in list...
            for (var i=0; i < ItemList.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var information = Instantiate(SampleInformationItem).GetComponent<SampleInformationMap>();

                // ... initialyze data.
                information.Name.text = ItemList[i].Name;
                information.Value.text = ItemList[i].Value;

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                information.transform.SetParent(ContentPanel, false);
            }
        }

        /// <summary><c>PopulateList</c> add data from <c>ItemList</c> into <c>ContentPanel</c>.</summary>
        public void PopulateList(IEnumerable<ItemInformation> items) {
            ItemList.AddRange(items);
            PopulateList();
        }
    }
}