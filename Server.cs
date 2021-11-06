using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{


    // public string buffer = "";
    private Socket server1;
    
    IPAddress ipAdd = IPAddress.Parse("192.168.3.22"); 
    
    IPAddress ipAddress = IPAddress.Parse(GetInsideIP(AddressFamily.InterNetwork));

    private List<Socket> clients;
    
    private Socket client;

    private IPEndPoint endPoint;

   // public int onlineNum = 0;
    
    //private byte[] databuf;
    
    //private int port = 6666;

    public static bool isSend = false;
    private void OnApplicationQuit()
    {
        server1.Close();
    }
    public void OnConnect()
    {
        server1.Bind(endPoint);
        server1.Listen(0);    
    }

    private void Awake()
    {
        clients = new List<Socket>();
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        server1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clients = new List<Socket>();
        endPoint = new IPEndPoint(ipAddress, 6666);
        //databuf = new byte[1024];
        OnConnect();
        
        Thread ThreadAccept = new Thread(Accept);
        ThreadAccept.IsBackground = true;
        ThreadAccept.Start();
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

    public void Accept()
    {
       client = server1.Accept();
       Thread ThreadReceive = new Thread(Receive);
       ThreadReceive.IsBackground = true;
       ThreadReceive.Start(client);
       Accept();

    }

    public void Receive(object obj)
    {  
        Socket Client = obj as Socket;
        IPEndPoint point = Client.RemoteEndPoint as IPEndPoint;
        try
        {
            byte[] ByteReceive = new byte[1024];
            int ReceiveLenght = Client.Receive(ByteReceive);
            Main.receiveBuff = Encoding.UTF8.GetString(ByteReceive, 0, ReceiveLenght);
            Receive(Client);
        }
        catch
        {
            clients.Remove(Client);
        }

    }

    public bool hasPlayer()
    {
        if (client != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void OnSend(string msg)
    {
        byte[] buf = Encoding.UTF8.GetBytes(msg);
        client.Send(buf);
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
