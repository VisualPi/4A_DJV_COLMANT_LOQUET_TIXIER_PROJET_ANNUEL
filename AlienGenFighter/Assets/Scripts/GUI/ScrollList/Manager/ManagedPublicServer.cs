using System.Collections.Generic;
using Assets.Scripts.GUI.Sample;
using Assets.Scripts.GUI.ScrollList.Item;
using UnityEngine;

namespace Assets.Scripts.GUI.ScrollList.Manager {
    public class ManagedPublicServer : ManagedScrollList<ItemServeurPublic> {
        [SerializeField]
        private GameObject SampleButton;

        protected override void _populateList(List<ItemServeurPublic> items) {
            // For each item in list...
            for (var i = 0; i < items.Count; ++i) {
                // We Instantiate a new gameObject and get component for ...
                var button = Instantiate(SampleButton).GetComponent<SampleButtonServer>();

                // ... initialyze data.
                button.Id = items[i].Id;
                button.ServerName.text = items[i].ServerName;
                button.IconServer.sprite = items[i].Icon;
                button.CurrentNbPlayer.text = items[i].CurrentNbPlayer;
                button.MaxNbPlayer.text = items[i].MaxNbPlayer;
                button.Ping.text = items[i].Ping;

                // Here we need copy for does not lost the item at the end of for.
                var item = items[i];
                button.Button.onClick.AddListener(delegate { item.DoWork(button.Id); });

                // In the end, we attache this new gameObject at ContentPanel.
                // We define false for not adapting the child to the parent. 
                button.transform.SetParent(ContentPanel, false);
            }
        }
    }
}
