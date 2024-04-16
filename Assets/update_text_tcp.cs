using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class update_text_tcp : MonoBehaviour
{
    public TextMeshProUGUI udp_text;
    private TCPSocket tcpSocket;
    // Start is called before the first frame update
    void Start()
    {
        tcpSocket = GetComponent<TCPSocket>();

    }

    // Update is called once per frame
    void Update()
    {
        udp_text.text = tcpSocket.receivedText;
    }
}
