using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

using PrettyNetworking.Utility;

namespace PrettyNetworking
{
    class NetworkHandler
    {
        private Socket _client;
        private DataHandler _dataHandler;

        public NetworkHandler()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void ConnectUsingSettings(string Ip, int Port)
        {
            try
            {
                _client.Connect(Ip, Port);
                _dataHandler = new DataHandler(_client);
            }
            catch
            {
                Debug.LogWarning("Failed to connect to server");
            }
        }
    }
}
