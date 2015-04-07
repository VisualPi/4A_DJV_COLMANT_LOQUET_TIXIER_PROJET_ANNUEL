using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Scripts.Network {
	public class GUI : MonoBehaviour {
		[SerializeField] private Manager _manager;

		//
		//private string	_titleMessage	= "AGF";
		//private string	_connectToIp	= "127.0.0.1";
		//private int		_connectionPort	= 26500;
		//private bool	_useNAT			= false;
		//private string	_ipAddress;
		//private string	_port;
		//private int		_numberOfPlayers	= 2;

		private bool	_showMainMenu						= false;
		private bool	_iWantToSetupAServer				= false;
		private bool	_iWantToConnectToAServer			= false;
		private bool	_iWantToSettle						= false;
		private bool	_iWantToSetupAPublicServer			= false;
		private bool	_iWantToSetupAPrivateServer			= false;
		private bool	_iWantToConnectToAPublicAServer		= false;
		private bool	_iWantToConnectToAPrivateAServer	= false;
		private bool	_searchPublicServer					= false;

		////////////////////
		// List Of Button //
		////////////////////

		////////
		// Main Window
		[Header("Main Window")]
		[SerializeField] private Button		_buttonSetupAServer;
		[SerializeField] private Button		_buttonConnectToAServer;
		[SerializeField] private Button		_buttonExitGame;
		[SerializeField] private Button		_buttonSetting;

		////////
		// Setup Server
		[Header("Setup Server")]
		[SerializeField] private Button		_buttonSetupAPublicServer;
		[SerializeField] private Button		_buttonSetupAPrivateServer;

		// Public
		[Header("Setup Server Public")]
		[SerializeField] private GameObject	_setupAPublicServerPanel;
		[SerializeField] private InputField	_inputFieldNameAPublicServer;
		[SerializeField] private Button		_buttonStartAPublicServer;

		// Private
		//[Header("Setup Server Private")]

		////////
		// Connect Server
		[Header("Connect Server")]
		[SerializeField] private Button		_buttonConnectToAPublicServer;
		[SerializeField] private Button		_buttonConnectToAPrivateServer;

		// Public
		[Header("Connect Public Server")]
		[SerializeField] private GameObject _connectToAPublicServerPanel;
		[SerializeField] private InputField _inputFieldPseudoForPublicServer;
		[SerializeField] private Button		_buttonRefreshListPublicServer;
		[SerializeField] private GameObject	_prefabPublicServer;
		[SerializeField] private GameObject	_PanelListPublicServer;


		// Private
		//[Header("Connect Private Server")]

		////////
		// Other
		[Header("Other")]
		public Button ButtonGoBack;

		void Start() {
			OnGoBack();
		}

		void Update() {
			// The user can to refresh the list of public server.
			if (_iWantToConnectToAPublicAServer && Input.GetKeyDown(KeyCode.R))
				RefreshListPublicServer();
		}

		void RefreshListPublicServer() {
			if (!_searchPublicServer) {
				_searchPublicServer = true;
				StartCoroutine(_manager.SearchPublicServer());
				_searchPublicServer = false;
			}
		}

		#region Fonction Display

		void HideMainPanel() {
			if (_showMainMenu) {
				_showMainMenu = !_showMainMenu;
				DisplayMainWindow();
			}
			if (_iWantToSetupAServer) {
				_iWantToSetupAServer = !_iWantToSetupAServer;
				DisplaySetupWindow();
			}
			if (_iWantToConnectToAServer) {
				_iWantToConnectToAServer = !_iWantToConnectToAServer;
				DisplayConnectWindow();
			}
			if (_iWantToSettle) {
				_iWantToSettle = !_iWantToSettle;
				DisplaySettingWindow();
			}
		}

		void HideAdditionaPanel() {
			if (_iWantToSetupAPublicServer) {
				_iWantToSetupAPublicServer = !_iWantToSetupAPublicServer;
				DisplaySetupPublicWindow();
			}
			if (_iWantToSetupAPrivateServer) {
				_iWantToSetupAPrivateServer = !_iWantToSetupAPrivateServer;
				DisplaySetupPrivateWindow();
			}
			if (_iWantToConnectToAPublicAServer) {
				_iWantToConnectToAPublicAServer = !_iWantToConnectToAPublicAServer;
				DisplayConnectToAPublicWindow();
			}
			if (_iWantToConnectToAPrivateAServer) {
				_iWantToConnectToAPrivateAServer = !_iWantToConnectToAPrivateAServer;
				DisplaySetupPrivateWindow();
			}
		}

		void DisplayMainWindow() {
			_buttonSetupAServer.gameObject.SetActive(_showMainMenu);
			_buttonConnectToAServer.gameObject.SetActive(_showMainMenu);
			_buttonSetting.gameObject.SetActive(_showMainMenu);

			if (!Application.isWebPlayer) {
				_buttonExitGame.gameObject.SetActive(_showMainMenu);
				if (Application.isEditor)
					_buttonExitGame.interactable = false;
			}
		}

		void DisplaySetupWindow() {
			_buttonSetupAPublicServer.gameObject.SetActive(_iWantToSetupAServer);
			_buttonSetupAPublicServer.interactable = true;

			_buttonSetupAPrivateServer.gameObject.SetActive(_iWantToSetupAServer);
			_buttonSetupAPrivateServer.interactable = true;

			ButtonGoBack.gameObject.SetActive(_iWantToSetupAServer);
		}

		void DisplayConnectWindow() {
			_buttonConnectToAPublicServer.gameObject.SetActive(_iWantToConnectToAServer);
			_buttonConnectToAPublicServer.interactable = true;

			_buttonConnectToAPrivateServer.gameObject.SetActive(_iWantToConnectToAServer);
			_buttonConnectToAPrivateServer.interactable = true;

			ButtonGoBack.gameObject.SetActive(_iWantToConnectToAServer);
		}

		void DisplaySettingWindow() {
			ButtonGoBack.gameObject.SetActive(_iWantToSettle);
		}

		void DisplaySetupPublicWindow() {
			_setupAPublicServerPanel.SetActive(_iWantToSetupAPublicServer);
			_inputFieldNameAPublicServer.text = _manager.ServerName;
		}

		void DisplaySetupPrivateWindow() {
		}

		void DisplayConnectToAPublicWindow() {
			_connectToAPublicServerPanel.SetActive(_iWantToConnectToAPublicAServer);
			_inputFieldPseudoForPublicServer.text = _manager.PlayerName;

			//RefreshListPublicServer();

			//if (_manager.HostDatas.Length == 0) {
			for (int i = 0; i < 10; i++) {
				
			}
			//}
		}

		void DisplayConnectToAPrivateWindow() {
		}

		#endregion Fonction Display

		#region Event Button MainPanel

		public void OnSetupAServer() {
			HideMainPanel();
			_iWantToSetupAServer = true;
			DisplaySetupWindow();
		}

		public void OnConnectToAServer() {
			HideMainPanel();
			_iWantToConnectToAServer = true;
			DisplayConnectWindow();
		}

		public void OnSetting() {
			HideMainPanel();
			_iWantToSettle = true;
			DisplaySettingWindow();
		}

		public void OnExitGame() {
			Application.Quit();
		}

		public void OnSetupAPublicServer() {
			HideAdditionaPanel();
			DisplaySetupWindow();
			_buttonSetupAPublicServer.interactable = false;

			_iWantToSetupAPublicServer = true;
			DisplaySetupPublicWindow();
		}

		public void OnSetupAPrivateServer() {
			HideAdditionaPanel();
			DisplaySetupWindow();
			_buttonSetupAPrivateServer.interactable = false;

			_iWantToSetupAPrivateServer = true;
			DisplaySetupPrivateWindow();
		}

		public void OnConnectToAPublicServer() {
			HideAdditionaPanel();
			DisplayConnectWindow();
			_buttonConnectToAPublicServer.interactable = false;

			_iWantToConnectToAPublicAServer = true;
			DisplayConnectToAPublicWindow();
		}

		public void OnConnectToAPrivateServer() {
			HideAdditionaPanel();
			DisplayConnectWindow();
			_buttonConnectToAPrivateServer.interactable = false;

			_iWantToConnectToAPrivateAServer = true;
			DisplayConnectToAPrivateWindow();
		}

		public void OnGoBack() {
			HideMainPanel();
			HideAdditionaPanel();
			_showMainMenu = true;
			DisplayMainWindow();
		}

		#endregion Event Button MainPanel

		#region Event Button SetupAPublicServerPanel

		public void OnSetupNamePublicServer() {
			if (_inputFieldNameAPublicServer.text == "")
				_buttonStartAPublicServer.interactable = false;
			else
				_buttonStartAPublicServer.interactable = true;
		}

		public void OnStartPublicServer() {
			// TODO : Start Public Server here
			_manager.StartPublicServer(_inputFieldNameAPublicServer.text);
		}

		#endregion

		#region Event Button ConnectToAPublicServerPanel

		public void OnSetupPseudoPublicServer() {
			//if (_inputFieldNameAPublicServer.text == "")
				// Todo : Disable connection of public server
		}

		public void OnRefreshListPublicServer() {
			RefreshListPublicServer();
		}

		#endregion
	}
}