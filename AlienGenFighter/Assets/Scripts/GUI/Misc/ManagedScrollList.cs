using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.GUI.Sample;

namespace Assets.Scripts.GUI.Misc {

	[System.Serializable]
	public class Item {
		public string ServerName;
		public Sprite Icon;
		public string CurrentNbPlayer;
		public string MaxNbPlayer;
		public string Ping;
		public Button.ButtonClickedEvent DoWork;
	}

	public class ManagedScrollList : MonoBehaviour {
		public GameObject SampleButton;
		public List<Item> ItemList;
		public Transform ContentPanel;

		/// <summary><c>ClearList</c> delete all data in <c>ContentPanel</c>.</summary>
		public void ClearList() {
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
				var button = Instantiate(SampleButton).GetComponent<SampleButtonServer>();

				// ... initialyze data.
				button.ServerName.text = ItemList[i].ServerName;
				button.IconServer.sprite = ItemList[i].Icon;
				button.CurrentNbPlayer.text = ItemList[i].CurrentNbPlayer;
				button.MaxNbPlayer.text = ItemList[i].MaxNbPlayer;
				button.Ping.text = ItemList[i].Ping;
				button.Button.onClick = ItemList[i].DoWork;

				// In the end, we attache this new gameObject at ContentPanel.
				// We define false for not adapting the child to the parent. 
				button.transform.SetParent(ContentPanel, false);
			}
		}

		public void OnDoWork() {
			Debug.Log("I do work.");
		}

		public void OnDoWork(string contante) {
			Debug.Log(contante);
		}
	}
}
