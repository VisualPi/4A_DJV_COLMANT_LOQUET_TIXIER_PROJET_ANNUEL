using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GUI.Sample;
using Assets.Scripts.Network.GUI;

namespace Assets.Scripts.GUI.Misc {

	[System.Serializable]
	public class Item {
		public string ServerName;
		public Sprite Icon;
		public string CurrentNbPlayer;
		public string MaxNbPlayer;
		public string Ping;
	}

	public class ManagedScrollList : MonoBehaviour {
		public GameObject SampleButton;
		public List<Item> ItemList;

		public Transform ContentPanel;

		void Start () {
			PopulateList();
		}

		private void PopulateList() {
			for (var i=0; i<ItemList.Count; ++i) {
				var newButton = Instantiate(SampleButton);
				var button = newButton.GetComponent<SampleButtonServer>();

				button.ServerName.text		= ItemList[i].ServerName;
				button.IconServer.sprite	= ItemList[i].Icon;
				button.CurrentNbPlayer.text = ItemList[i].CurrentNbPlayer;
				button.MaxNbPlayer.text		= ItemList[i].MaxNbPlayer;
				button.Ping.text			= ItemList[i].Ping;

				button.transform.SetParent(ContentPanel, false);
			}
		}
	}
}
