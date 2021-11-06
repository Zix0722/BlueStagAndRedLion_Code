using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    public Button Host;

    public Button Connect;

    public InputField ipAdd;

    public GameObject ServerPrefab;

    public GameObject ClientPrefab;

    public Text Tip;
    // Start is called before the first frame update
    void Start()
    {
        Host.onClick.AddListener(onHostClick);
        Connect.onClick.AddListener(onConnectClick);
    }

    private void onHostClick()
    {
        ServerPrefab.GameObject().SetActive(true);
        Main.player = 1;
    }

    private void onConnectClick()
    {
        ClientPrefab.GameObject().SetActive(true);
        Main.player = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (ServerPrefab.GameObject().GetComponent<Server>().hasPlayer())
        {
            Tip.text = "There's a player in your room, ready to play...";
            StartCoroutine(holdSec());
            Main.buffer = "ready";
            Server.isSend = true;
        }

        if (Main.receiveBuff == "ready")
        {
            Tip.text = "Ready for the game to begin....";
            StartCoroutine(holdSec());
        }
    }

    IEnumerator holdSec()
    {
        yield return new WaitForSeconds(5f);
        this.GameObject().SetActive(false);
        Main.isGameReady = true;
    }
    
}
