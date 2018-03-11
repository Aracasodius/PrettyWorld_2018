using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Debugging;

namespace PrettyNetworking
{
    class Server
    {
        private Server _instance;
        private Socket _server;
        private int _maxClients = 20;
        private int _clientCount = 0;

        private List<Socket> _currentClients;
        public List<Socket> ClientList
        {
            get { return _currentClients; }
        }

        public Server()
        {
            _currentClients = new List<Socket>();

            _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(new IPEndPoint(IPAddress.Any, 4379));
            _server.Listen(_maxClients);

            _instance = this;

            Logger.Log(">> Server Started\n");
            StartConnection();
        }

        public void Restart()
        {

        }
        public void Close()
        {
            _currentClients.Clear();
            _server.Dispose();

            Logger.Log(">> Server has been shut down");
        }

        public void Subscribe(Socket Client)
        {
            _currentClients.Add(Client);

            Logger.Log(">> New connection started with " + ((IPEndPoint)(Client.RemoteEndPoint)).Address);
        }
        public void Unsubscribe(Socket Client)
        {
            _currentClients.Remove(Client);

            Logger.Log(">> Connection with " + ((IPEndPoint)(Client.RemoteEndPoint)).Address + " has been closed");
        }

        private void StartConnection()
        {
            _server.BeginAccept(new AsyncCallback(AcceptConnection), null);
        }
        private void AcceptConnection(IAsyncResult result)
        {
            Socket clientSocket = _server.EndAccept(result);
            _clientCount++;

            ClientHandler client = new ClientHandler(clientSocket, _clientCount, ref _instance);
            StartConnection();
        }
    }
}
