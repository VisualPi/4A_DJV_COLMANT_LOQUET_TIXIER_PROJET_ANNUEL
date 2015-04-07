using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UENetwork = UnityEngine.Network;

namespace Assets.Scripts.Network {
	public class Manager : MonoBehaviour {

		//
		private string _titleMessage = "AGF";
		private string _connectToIp = "127.0.0.1";
		private int _connectionPort = 26500;
		private bool _useNAT = false;
		private string _ipAddress;
		private string _port;
		private int _numberOfPlayers = 2;
		[SerializeField]
		private string _playerName;
		[SerializeField]
		private string _serverName;
		[SerializeField]
		private string _serverNameForClient;

		//
		private string _gameNameType = "AGFServer ESGI 4A DJV2";
		private Ping _masterServerPing;
		private HostData[] _hostDatas;
		private string _ipString;
		private List<Ping> _serverPingsList = new List<Ping>();
		private bool _noPublicServer = false;

		// Use this for initialization
		private void Start() {
			// Set server name
			_serverName = PlayerPrefs.GetString("serverName");

			if (_serverName == "") // if not always use, set as default name.
				_serverName = "Server";

			// Ping the master server to find out how long it takes to communicate to it.
			MasterServer.RequestHostList(_gameNameType);

			_masterServerPing = new Ping(MasterServer.ipAddress);
		}

		#region Geter Seter

		public string ServerName {
			get {
				return _serverName;
			}
			set {
				if (value != "") {
					PlayerPrefs.SetString("serverName", _serverName);
					_serverName = value;
				}
			}
		}

		public string PlayerName {
			get {
				return _playerName;
			}
			set {
				if (value != "") {
					PlayerPrefs.SetString("playerName", _playerName);
					_playerName = value;
				}
			}
		}

		public HostData[] HostDatas { get { return _hostDatas; } }

		public List<Ping> ServerPingsList { get { return _serverPingsList; } }

		#endregion Geter Seter

		#region Setup Server

		public bool StartPublicServer(string name = null) {
			if (name != null)
				ServerName = name;

			UENetwork.InitializeServer(_numberOfPlayers, _connectionPort, !UENetwork.HavePublicAddress());
			MasterServer.RegisterHost(_gameNameType, _serverName, "");

			// Display Manage Game
			return true;
		}

		public IEnumerator SearchPublicServer() {
			_hostDatas = new HostData[0];

			MasterServer.ClearHostList();
			MasterServer.RequestHostList(_gameNameType);

			yield return new WaitForSeconds(_masterServerPing.time / 100 + 0.1f);

			_hostDatas = MasterServer.PollHostList();

			_serverPingsList.Clear();
			_serverPingsList.TrimExcess();

			for (int i = 0; i < _hostDatas.Length; i++)
				_serverPingsList.Add(new Ping(_hostDatas[i].ip[0]));
		}

		public bool ConnectToAPublicServer(string pseudo = null) {
			if (pseudo != null)
				ServerName = pseudo;

			return true;
		}

		#endregion Setup Server

		#region Event Unity

		//////////
		// Client and Server

		void OnDisconnectedFromServer() { }


		//////////
		// Server

		void OnPlayerDisconnected(NetworkPlayer networkPlayer) { }
		void OnPlayerConnected(NetworkPlayer networkPlayer) { }


		//////////
		// Client

		void OnConnectedToServer() { }

		#endregion Event Unity
	}
}

