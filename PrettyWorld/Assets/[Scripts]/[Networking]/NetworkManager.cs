using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrettyNetworking
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string _ipAddress;
        [SerializeField] private int _port;

        private NetworkHandler _networkHandler;

        private void Awake()
        {
            _networkHandler = new NetworkHandler();
        }

        public void ConnectToServer()
        {
            _networkHandler.ConnectUsingSettings(_ipAddress, _port);
        }
    }
}
