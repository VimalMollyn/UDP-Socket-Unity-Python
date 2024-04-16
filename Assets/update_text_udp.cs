using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class update_text_udp : MonoBehaviour
{
    public TextMeshProUGUI udp_text;
    private UDPSocket udpsocket;
    // Start is called before the first frame update
    void Start()
    {
        udpsocket = GetComponent<UDPSocket>();

    }

    // Update is called once per frame
    void Update()
    {
        udp_text.text = udpsocket.receivedText;
    }
}
