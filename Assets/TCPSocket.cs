using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TCPSocket : MonoBehaviour
{
    // Start is called before the first frame update

    private Socket tcpSocket;

    [SerializeField]
    public string serverIP = "127.0.0.1";

    [SerializeField]
    public int serverPort = 8888; // Server port

    public string receivedText = "";

    void Start()
    {
        try
        {
            // Create a TCP socket
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the server
            tcpSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
            Debug.Log($"Connected to server: {serverIP}:{serverPort}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to connect to server: {e.Message}");
        }

    }

    void Update()
    {
        // Send a random integer to the server every frame
        int randomInt = UnityEngine.Random.Range(0, 100);

        // send every 60 frames
        if (Time.frameCount % 60 == 0)
        {
            SendMessage(randomInt.ToString());
        }


    }

    private void SendMessage(string message)
    {
        try
        {
            // Convert the message to bytes
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Send the message to the server
            tcpSocket.Send(data);
            Debug.Log($"Sent message: {message}");

            // Receive the response from the server
            byte[] receiveBuffer = new byte[1024];
            int receivedBytes = tcpSocket.Receive(receiveBuffer);
            // Convert byte array to string
            receivedText = Encoding.UTF8.GetString(receiveBuffer, 0, receivedBytes);
            Debug.Log($"Received: {receivedText}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to send message: {e.Message}");
        }
    }

    private void OnDestroy()
    {
        // Close the socket when the script is destroyed
        if (tcpSocket != null && tcpSocket.Connected)
        {
            tcpSocket.Close();
        }
    }

    void OnApplicationQuit()
    {
        if (tcpSocket != null)
        {
            tcpSocket.Close();
        }

    }
}