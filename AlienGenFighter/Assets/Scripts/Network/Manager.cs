using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UENetwork = UnityEngine.Network;
using Assets.Scripts.GUI.ScrollList.Item;
using Assets.Scripts.GUI.ScrollList.Manager;

namespace Assets.Scripts.Network
{
    public class Manager : MonoBehaviour
    {
        //
        [SerializeField]
        private ManagedPublicServer _scrollListManager;

        //
        private string  _titleMessage = "AGF";
        private string  _connectToIp = "127.0.0.1";
        private int     _connectionPort = 26500;
        private bool    _useNAT = false;
        private string  _ipAddress;
        private string  _port;
        private int     _numberOfPlayers = 2;
        private string  _playerName;
        private string  _serverName;
        private string  _serverNameForClient;

        //
        private string _gameNameType = "AGFServer ESGI 4A DJV2";
        private Ping _masterServerPing;
        private HostData[] _hostDatas;
        private string _ipString;
        //private List<Ping> _serverPingsList = new List<Ping>();
        //private bool _noPublicServer = false;

        // Use this for initialization
        private void Start()
        {
            // Set server name
            _serverName = PlayerPrefs.GetString("serverName");

            if ( _serverName == "" ) // if not always use, set as default name.
                _serverName = "Server";

            // Set player name
            _playerName = PlayerPrefs.GetString("playerName");

            if ( _playerName == "" ) // if not always use, set as default name.
                _playerName = "Player";

            // Ping the master server to find out how long it takes to communicate to it.
            MasterServer.RequestHostList(_gameNameType);

            _masterServerPing = new Ping(MasterServer.ipAddress);
        }

        #region Geter Seter

        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                if ( value != "" )
                {
                    PlayerPrefs.SetString("serverName", _serverName);
                    _serverName = value;
                }
            }
        }

        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                if ( value != "" )
                {
                    PlayerPrefs.SetString("playerName", _playerName);
                    _playerName = value;
                }
            }
        }

        #endregion Geter Seter

        #region Setup Server

        public void StartPublicServer()
        {
            UENetwork.InitializeServer(_numberOfPlayers, _connectionPort, !UENetwork.HavePublicAddress());
            MasterServer.RegisterHost(_gameNameType, _serverName, "");
        }

        public IEnumerator SearchPublicServer()
        {
            // Clear the list of servers
            _scrollListManager.RemoveAll();
            MasterServer.ClearHostList();
            MasterServer.RequestHostList(_gameNameType);

            // Wait for the host list is retrieved from the Master Server
            yield return new WaitForSeconds(_masterServerPing.time / 100 + 0.1f);

            // The list of public servers has been retrieved .
            _hostDatas = MasterServer.PollHostList();

            // We process information to be displayed
            var items = new List<ItemServeurPublic>();
            for ( int i = 0 ; i < _hostDatas.Length ; i++ )
            {
                items.Add(new ItemServeurPublic()
                {
                    Id = i,
                    CurrentNbPlayer = _hostDatas[i].connectedPlayers.ToString(),
                    MaxNbPlayer = _hostDatas[i].playerLimit.ToString(),
                    Ping = new Ping(_hostDatas[i].ip[0]).ToString(),
                    ServerName = _hostDatas[i].gameName,
                    DoWork = ConnectToAPublicServer
                });
            }

            _scrollListManager.PopulatePanel(items);
        }

        public void ConnectToAPublicServer(int id)
        {
            Debug.Log(id);
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

