﻿using System;
using Network;
using Network.Enums;

namespace ServerClient
{
    public class Server
    {
        public void Main()
        {
            //1. Create a new server container.
            ServerConnectionContainer serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(8888, false);
            //2. Apply some settings
            serverConnectionContainer.AllowUDPConnections = true;
            //3. Set a delegate which will be called if we receive a connection
            serverConnectionContainer.ConnectionEstablished += ServerConnectionContainer_ConnectionEstablished;
            //4. Set a delegate which will be called if we lose a connection
            serverConnectionContainer.ConnectionLost += ServerConnectionContainer_ConnectionLost;
            //4. Start listening on port 1234
            serverConnectionContainer.StartTCPListener();

            Console.ReadLine();
        }

        private void ServerConnectionContainer_ConnectionLost(Connection connection, ConnectionType connectionType, CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ServerConnectionContainer_ConnectionEstablished(Connection connection, ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
        }
    }
    public class Client
    {
        public void Main()
        {
            //1. Create a new client connection container.
            ClientConnectionContainer clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer("192.168.1.104", 8888);
            //2. Setup events which will be fired if we receive a connection
            clientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            clientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;
        }

        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
        }
    }
}