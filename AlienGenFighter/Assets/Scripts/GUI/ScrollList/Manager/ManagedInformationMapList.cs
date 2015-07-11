using System.Collections.Generic;
using Assets.Scripts.GUI.Sample;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Manager {
    public class ManagedInformationMapList : ManagedScrollList<ItemInformationMap> {
        [SerializeField]
        private GameObject SampleButton;

        protected override void _populateList(List<ItemInformationMap> items) {
            // For each item in list...
            for (var i = 0; i < items.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var information = Instantiate(SampleButton).GetComponent<SampleInformationMap>();

                // ... initialyze data.
                information.Name.text = items[i].Name;
                information.Value.text = items[i].Value;

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                information.transform.SetParent(ContentPanel, false);
            }
        }
    }
}