using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class UDPSocket : MonoBehaviour
{
    // Start is called before the first frame update

    private Socket udpSocket;
    private IPEndPoint localEndPoint;
    private IPEndPoint remoteEndPoint;
    private bool isListening = true;
    private Thread receiveThread;

    // [SerializeField]
    // private string serverIP = "127.0.0.1";

    [SerializeField]
    private int serverPort = 8888; // Server port

    public string receivedText = "";

    void Start()
    {
        udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // remoteEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        localEndPoint = new IPEndPoint(IPAddress.Any, serverPort);
        udpSocket.Bind(localEndPoint);

        Debug.Log("Socket created");
        // Debug.Log("Server IP: " + serverIP);
        Debug.Log("Server port: " + serverPort);

        // receive data from the udpSocket in a different thread
        Thread receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space)) // For example, send message when Space is pressed
        // {
        //     SendMessage("Hello from Unity!");
        // }
    }

    private void ReceiveData()
    {
        while (isListening)
        {
            try
            {
                // Buffer for incoming data
                byte[] receiveBuffer = new byte[1024];
                EndPoint senderRemote = new IPEndPoint(IPAddress.Any, 0);

                // Receive data
                int receivedBytes = udpSocket.ReceiveFrom(receiveBuffer, ref senderRemote);

                // Convert byte array to string
                receivedText = Encoding.UTF8.GetString(receiveBuffer, 0, receivedBytes);

                // Handle received data on the main thread
                Debug.Log("Received: " + receivedText);
            }
            catch (Exception err)
            {
                Debug.LogError(err.ToString());
            }
        }
    }

    void SendMessage(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            udpSocket.SendTo(data, remoteEndPoint);
            Debug.Log("Message sent: " + message);
        }
        catch (Exception err)
        {
            Debug.LogError(err.ToString());
        }
    }

    void OnApplicationQuit()
    {
        isListening = false;

        if (udpSocket != null)
        {
            udpSocket.Close();
        }

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort();
        }
    }
}
