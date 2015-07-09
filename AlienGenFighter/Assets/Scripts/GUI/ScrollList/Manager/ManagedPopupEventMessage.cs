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

        protected override void _populateList() {
            // For each item in list...
            for (var i = 0; i < ItemList.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var button = Instantiate(_sampleButton).GetComponent<SamplePopupEvent>();

                // ... initialyze data.
                button.Icon.sprite = ItemList[i].Icon;
                button.Title.text = ItemList[i].Title;
                button.Description.text = ItemList[i].Description;
                button.Timestamp.text = ItemList[i].Timestamp;

                // Here we need copy for does not lost the item at the end of for.
                var item = ItemList[i];
                button.ButtonEvent.onClick.AddListener(delegate { item.DoWorkEvent(); });
                button.ButtonRemoveEvent.onClick.AddListener(() => RemoveItem(item));

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                button.transform.SetParent(ContentPanel, false);
            }

            if (ItemList.Count > 0)
                _menu.IsOpen = true;
        }
    }
}