using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;

namespace PrettyNetworking.Utility
{
    class DataHandler
    {
        private Socket _masterSocket;
        private int _masterID;
        private byte[] _buffer;

        public DataHandler(Socket socket)
        {
            _masterSocket = socket;

            _buffer = new byte[1024];
            ReceiveData();
        }

        private void ReceiveData()
        {
            _masterSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _masterSocket);
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            int bytes = _masterSocket.EndReceive(result);
            byte[] data = new byte[bytes];
            Array.Copy(_buffer, data, bytes);
            
            Debug.Log("Received a new data package\n" +
                "Typeof: PackageType." + ((PackageType)BitConverter.ToInt32(data, 0)));

            HandlePacket(data);
        }
        
        public void SendPacketToOne(PackageType packageType, byte[] Data, int ID)
        {
            ByteBuffer byteBuffer = new ByteBuffer();

            switch (packageType)
            {
                case PackageType.ConnectData:

                    byteBuffer.WriteInt((int)packageType);
                    byteBuffer.WriteInt(ID);

                    _masterSocket.BeginSend(byteBuffer.ToArray(), 0, byteBuffer.Count(), SocketFlags.None, new AsyncCallback(SendCallback), _masterSocket);
                    break;
            }
        }
        private void SendCallback(IAsyncResult result)
        {
            Socket callbackSocket = (Socket)result.AsyncState;
            callbackSocket.EndSend(result);
        }

        private void HandlePacket(byte[] Data)
        {
            ByteBuffer byteBuffer = new ByteBuffer();
            byteBuffer.WriteBytes(Data);

            switch ((PackageType)byteBuffer.ReadInt())
            {
                case PackageType.ConnectData:

                    _masterID = byteBuffer.ReadInt();
                    break;
            }
        }
    }
}
