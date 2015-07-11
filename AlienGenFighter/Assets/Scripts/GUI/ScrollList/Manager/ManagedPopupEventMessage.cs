using System.Collections.Generic;
using Assets.Scripts.GUI.Sample;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Manager {
    public class ManagedPopupEventMessage : ManagedScrollList<ItemEvent> {
        [SerializeField]
        private GameObject _sampleButton;
        [SerializeField]
        private Menu _menu;


        protected override void ClearPanel() {
            _menu.IsOpen = false;
            base.ClearPanel();
        }

        protected override void _populateList(List<ItemEvent> items) {
            // For each item in list...
            for (var i = 0; i < items.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var button = Instantiate(_sampleButton).GetComponent<SamplePopupEvent>();

                // ... initialyze data.
                button.Icon.sprite = items[i].Icon;
                button.Title.text = items[i].Title;
                button.Description.text = items[i].Description;
                button.Timestamp.text = items[i].Timestamp;

                // Here we need copy for does not lost the item at the end of for.
                var item = items[i];
                button.ButtonEvent.onClick.AddListener(delegate { item.DoWorkEvent(); });
                button.ButtonRemoveEvent.onClick.AddListener(() => { RemoveItem(item); RefreshPanel(); });
                button.ActionEvent.AddListener(() => RemoveItem(item));

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                button.transform.SetParent(ContentPanel, false);
                button.StartTimer(3f);
            }

            if (ItemList.Count > 0)
                _menu.IsOpen = true;
        }
    }
}