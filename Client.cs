using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Socket client; 

    private string data;
    
    public static bool isSend = false;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(GetInsideIP(AddressFamily.InterNetwork), 6666);
            
            Thread ThreadReceive = new Thread(Receive);
            ThreadReceive.IsBackground = true;
            ThreadReceive.Start();
            OnSend("Send a message to the server");
            Debug.Log("connect succesfully");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSend)
        {
            OnSend(Main.buffer);
            isSend = false;
            Main.buffer = null;
        }
    }

    public void OnSend(string msg)
    {
        byte[] buf = Encoding.UTF8.GetBytes(msg);
        client.Send(buf);
    }
    
    private void Receive()
    {
        try
        {
            byte[] ByteReceive = new byte[1024];
            int ReceiveLenght = client.Receive(ByteReceive);
            Main.receiveBuff = Encoding.UTF8.GetString(ByteReceive, 0, ReceiveLenght);
            Receive();
        }
        catch (Exception e)
        {
            // Debug.Log(e.Message);
        }
    }
    
    private static string GetInsideIP(AddressFamily addressType)
    {
        IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
        foreach (var item in ips)
        {
            if (item.AddressFamily == addressType)
            {
                return item.ToString();
            }
        }
        return null;
    }
}
