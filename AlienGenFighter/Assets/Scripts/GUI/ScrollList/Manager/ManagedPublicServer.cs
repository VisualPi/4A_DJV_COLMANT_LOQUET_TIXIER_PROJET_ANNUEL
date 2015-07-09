using Assets.Scripts.GUI.Sample;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Manager {
    public class ManagedPublicServer : ManagedScrollList<ItemServeurPublic> {
        [SerializeField]
        private GameObject SampleButton;

        protected override void _populateList() {
            // For each item in list...
            for (var i = 0; i < ItemList.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var button = Instantiate(SampleButton).GetComponent<SampleButtonServer>();

                // ... initialyze data.
                button.Id = ItemList[i].Id;
                button.ServerName.text = ItemList[i].ServerName;
                button.IconServer.sprite = ItemList[i].Icon;
                button.CurrentNbPlayer.text = ItemList[i].CurrentNbPlayer;
                button.MaxNbPlayer.text = ItemList[i].MaxNbPlayer;
                button.Ping.text = ItemList[i].Ping;

                // Here we need copy for does not lost the item at the end of for.
                var item = ItemList[i];
                button.Button.onClick.AddListener(delegate { item.DoWork(button.Id); });

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                button.transform.SetParent(ContentPanel, false);
            }
        }
    }
}
