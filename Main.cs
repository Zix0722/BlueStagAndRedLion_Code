using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Main : MonoBehaviour
{
    public List<GameObject> AllHumans;
    public  List<GameObject> Player1Humans;
    public  List<GameObject> Player2Humans;
    public MapSp areaA;
    public MapSp areaB;
    public MapSp areaC;
    public Button goBtn;
    public GameObject warning;
    public Canvas StartUI;
    public GameObject countDown;
    public GameObject Arrow;
    [SerializeField] GameObject fig_sword;
    [SerializeField] GameObject fig_spear;
    [SerializeField] GameObject fig_Bow;
    [SerializeField] GameObject fig1_sword;
    [SerializeField] GameObject fig1_spear;
    [SerializeField] GameObject fig1_Bow;
    public GameObject Bar1;
    public GameObject Bar2;
    private int chessNum = 11;

    public static int player = 0;
    public static string buffer = "";
    public static string receiveBuff = "";
    public static bool isGameReady = false;

    private bool p1Done = false;
    private bool p2Done = false;
    
    [HideInInspector]
    public static int P1HumanNum = 1;
    [HideInInspector]
    public static int P2HumanNum = 1;

    private List<GameObject> selectedList;
    private int mark = 0;
    
    

    // Start is called before the first frame update
    void Start()
    { 
        Init();
        selectedList = new List<GameObject>();
        goBtn.onClick.AddListener(goBtnClick);
        StartUI.GameObject().SetActive(true);
        foreach (var human in Player1Humans)
        {
            AllHumans.Add(human);
        }
        foreach (var human in Player2Humans)
        {
            AllHumans.Add(human);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        countStart(isGameReady);
        //
        click();
        switch (player)
        {
            case 1 :
                if(!p2Done)
                    p2Done = receiveMsgThen();
                break;
            case 2:
                if(!p1Done)
                    p1Done = receiveMsgThen();
                break;
        }
        if (p1Done && p2Done)
        {
            doAllMove();
            StartCoroutine(holdSec());
            //doMapCheck();
            //supplyChess();   
            
            Debug.Log("gonna do MapCheck");
            Debug.Log("P1:: "+ Player1Humans.Count);
            Debug.Log("P2:: "+ Player2Humans.Count);
            p1Done = false;
            p2Done = false;
            
        }
        
        

        /*if (Input.GetKeyDown(KeyCode.W))
        {
            doMapCheck();
        }*/
    }

    private bool allHumanMoveDone()
    {
        foreach (var human in AllHumans)
        {
            if (human.GetComponent<HumanMove>().moveDone == false)
            {
                return false;
            }
        }

        return true;
    }

    private void setAllHumansMoveNotDone()
    {
        foreach (var human in AllHumans)
        {
            human.GetComponent<HumanMove>().moveDone = false;
        }
    }

    private void doAllMove()
    {
        areaA.allMove();
        areaB.allMove();
        areaC.allMove();
    }

    private Vector3 findPlace()
    {
        var collection = Bar1.GetComponent<Barrack>().humanPoints;
        for (int i = 0; i < collection.Count; i++)
        {
            if (!collection[i].GetComponent<humanPoint>().humanOn)
            {
                collection[i].GetComponent<humanPoint>().humanOn = true;
                return new Vector3(collection[i].transform.position.x,collection[i].transform.position.y+7,collection[i].transform.position.z);
            } 
        }

        return new Vector3(0,0,0);
    }  // for player 1
    private Vector3 findPlace2()
    {
        var collection = Bar2.GetComponent<Barrack>().humanPoints;
        for (int i = 0; i < collection.Count; i++)
        {
            if (!collection[i].GetComponent<humanPoint>().humanOn)
            {
                collection[i].GetComponent<humanPoint>().humanOn = true;
                return  new Vector3(collection[i].transform.position.x,collection[i].transform.position.y+7,collection[i].transform.position.z);
            } 
        }

        return new Vector3(0,0,0);
    } // for player 2

    private void supplyChess()
    {
        switch (areaA.OccupiedBy)
        {
            case 0:
                break;
            case 1:
                GameObject newChess = Instantiate(fig_Bow,  findPlace(),GameObject.Find("Barracks").transform.rotation);
                newChess.transform.parent = GameObject.Find("Barracks").transform;
                chessNum++;
                newChess.tag = "P1";
                newChess.transform.GetChild(0).tag = "O";
                newChess.name = "fig ("+chessNum+")";
                Player1Humans.Add(newChess);
                AllHumans.Add(newChess);
                
                break;
            case 2:
                GameObject newChess2 = Instantiate(fig1_Bow,  findPlace2(),GameObject.Find("Barracks 2").transform.rotation);
                newChess2.transform.parent = GameObject.Find("Barracks 2").transform;
                chessNum++;
                newChess2.tag = "P2";
                newChess2.transform.GetChild(0).tag = "O";
                newChess2.name = "fig ("+chessNum+")";
                Player2Humans.Add(newChess2);
                AllHumans.Add(newChess2);
                break;
            
        }
        switch (areaB.OccupiedBy)
        {
            case 0:
                break;
            case 1:
                GameObject newChess = Instantiate(fig_spear,  findPlace(),GameObject.Find("Barracks").transform.rotation);
                newChess.transform.parent = GameObject.Find("Barracks").transform;
                chessNum++;
                newChess.tag = "P1";
                newChess.transform.GetChild(0).tag = "Y";
                newChess.name = "fig ("+chessNum+")";
                Player1Humans.Add(newChess);
                AllHumans.Add(newChess);
                break;
            case 2:
                GameObject newChess2 = Instantiate(fig1_spear,  findPlace2(),GameObject.Find("Barracks 2").transform.rotation);
                newChess2.transform.parent = GameObject.Find("Barracks 2").transform;
                chessNum++;
                newChess2.tag = "P2";
                newChess2.transform.GetChild(0).tag = "Y";
                newChess2.name = "fig ("+chessNum+")";
                Player2Humans.Add(newChess2);
                AllHumans.Add(newChess2);
                break;
            
        }
        switch (areaC.OccupiedBy)
        {
            case 0:
                break;
            case 1:
                GameObject newChess = Instantiate(fig_sword,  findPlace(),GameObject.Find("Barracks").transform.rotation);
                newChess.transform.parent = GameObject.Find("Barracks").transform;
                chessNum++;
                newChess.tag = "P1";
                newChess.transform.GetChild(0).tag = "@";
                newChess.name = "fig ("+chessNum+")";
                Player1Humans.Add(newChess);
                AllHumans.Add(newChess);
                break;
            case 2:
                GameObject newChess2 = Instantiate(fig1_sword,  findPlace2(),GameObject.Find("Barracks 2").transform.rotation);
                newChess2.transform.parent = GameObject.Find("Barracks 2").transform;
                chessNum++;
                newChess2.tag = "P2";
                newChess2.transform.GetChild(0).tag = "@";
                newChess2.name = "fig ("+chessNum+")";
                Player2Humans.Add(newChess2);
                AllHumans.Add(newChess2);
                break;
            
        }
    }

    private void ArrowWakeUp(Vector3 chessPos, Quaternion rot)
    {
        Arrow.transform.position = chessPos;
        Arrow.SetActive(true); 
    }
    void click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
            RaycastHit hit; 
            if (Physics.Raycast(ray, out hit, 10000f))        
            {
                switch(player)
                {
                    case 1:
                        if (hit.transform.name == "base" && hit.transform.parent.tag == "P1")
                        {
                            // StartCoroutine(CubeClickIen(hit.point));
                            ArrowWakeUp(hit.transform.parent.position, hit.transform.parent.rotation);
                            if (selectedList.Count==1)
                            {
                                if (selectedList[0] == hit.transform.parent.GameObject())
                                {
                                   selectedList.Remove(hit.transform.parent.GameObject());
                                   Bar1.GetComponent<Barrack>().humanPoints[Player1Humans.IndexOf(hit.transform.parent.GameObject())].GetComponent<humanPoint>().humanOn = true;
                                   Player1Humans.Add(hit.transform.parent.GameObject()); 
                                }
                            }
                            else
                            {
                                if (!isDone2() && selectedList.Count < P1HumanNum && Player1Humans.Contains(hit.transform.parent.GameObject()))
                                {
                                    selectedList.Add(hit.transform.parent.GameObject());
                                    Bar1.GetComponent<Barrack>().humanPoints[Player1Humans.IndexOf(hit.transform.parent.GameObject())].GetComponent<humanPoint>().humanOn = false;
                                    Player1Humans.Remove(hit.transform.parent.GameObject());
                                    
                                    Debug.Log("p1 has"+Player1Humans.Count);
                        
                                }
                                else
                                {
                                    warning.GameObject().SetActive(true);
                                    warning.GetComponent<warning>().setText("You have completed this round of deployment.");
                                }
                                              
                            }
                            Debug.Log("There are" + selectedList.Count + "in the selectedList");
                        }

                        break;
                     case 2:
                         if (hit.transform.name == "base" && hit.transform.parent.tag == "P2")
                         {
                             // StartCoroutine(CubeClickIen(hit.point));
                             ArrowWakeUp(hit.transform.parent.position, hit.transform.parent.rotation);
                             if (selectedList.Count==1)
                             {
                                 if (selectedList[0] == hit.transform.parent.GameObject())
                                 {
                                     selectedList.Remove(hit.transform.parent.GameObject());
                                     Bar2.GetComponent<Barrack>().humanPoints[Player2Humans.IndexOf(hit.transform.parent.GameObject())].GetComponent<humanPoint>().humanOn = true;
                                     Player2Humans.Add(hit.transform.parent.GameObject());
                                 }
                             }
                             else
                             {
                                 if (!isDone2() && selectedList.Count < P2HumanNum && Player2Humans.Contains(hit.transform.parent.GameObject()))
                                 {
                                     selectedList.Add(hit.transform.parent.GameObject());
                                     Bar2.GetComponent<Barrack>().humanPoints[Player2Humans.IndexOf(hit.transform.parent.GameObject())].GetComponent<humanPoint>().humanOn = false;
                                     Player2Humans.Remove(hit.transform.parent.GameObject());
                                     Debug.Log("P2 has"+Player2Humans.Count);
                        
                                 }
                                 else
                                 {
                                     warning.GameObject().SetActive(true);
                                     warning.GetComponent<warning>().setText("You have completed this round of deployment.");
                                 }
                                              
                             }
                             Debug.Log("There are " + selectedList.Count + "in the selectedList");
                         }

                         break;
                }
                

                if (hit.transform.parent.name.Contains("Area"))
                {
                    if (!hit.transform.GetComponent<point>().hasManIn)
                    {
                       foreach (var t in selectedList)
                       {
                           hit.transform.GetComponent<point>().AttHumanList.Add(t);
                           buffer = buffer + t.name + "|" + hit.transform.parent.name+ "|" + hit.transform.name + ",";
                           Arrow.SetActive(false);                     
                       }
                       selectedList.Clear();
                       //Debug.Log("攻击有" + hit.transform.parent.GetComponent<MapSp>().AttHumanList.Count);
                       Debug.Log("buff: " + buffer); 
                    }
                    else
                    {
                        warning.GameObject().SetActive(true);
                        warning.GetComponent<warning>().setText("You cannot send to the location where a soldier already exist");
                    }
                    
                }

            }
        }
    }
    
    private bool isDone2()
    {
        int cnt = 0;
        if (buffer == null)
        {
            return false;
        }
        foreach (var _char in buffer)
        {
            if (_char == ',')
            {
                cnt++;
            }
        }

        if (Player1Humans.Count < 2 && cnt == 1)
        {
            return true;
        }

        if (cnt == 2)
        {
            return true;
        }

        return false;
    }
    void goBtnClick()
    {
        
        if (isDone2())
        {
          switch (player)
                  {
                      case 1:
                          p1Done = true;
                          countDown.GetComponent<CountDown>().HangOn();
                          break;
                      case 2:
                          p2Done = true;
                          countDown.GetComponent<CountDown>().HangOn();
                          break;
                  }
                  if (player == 1)
                  {
                      Server.isSend = true;
                  }
          
                  if (player == 2)
                  {
                      Client.isSend = true;
                  }  
        }
        else
        {
            
            warning.GameObject().SetActive(true);
            warning.GetComponent<warning>().setText("More soldiers need to be sent to end the round.");
            //Debug.Log("cao ");
        }
        
    }

    void Init()
    {
        foreach (var t in Player1Humans)
        {
            t.gameObject.tag = "P1";
        }
        foreach (var t in Player2Humans)
        {
            t.gameObject.tag = "P2";
        }
    }

    void doMapCheck()
    {
        areaA.MapCheck();
        areaB.MapCheck();
        areaC.MapCheck();
    }

    bool receiveMsgThen()
    {
        if (receiveBuff != null && receiveBuff.Contains('|'))
        {
            string[] temp = receiveBuff.Split(',');
            List<string[]> temp2 = new List<string[]>();
            for (int i = 0; i < temp.Length-1; i++)
            {
                 temp2.Add(temp[i].Split('|'));
            }

            foreach (var item in temp2)
            {
                
                switch (item[1])
                {
                    case "AreaA":
                        switch (item[2])
                        {
                            case "pointA":
                               areaA.pointA.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0]));
                               if (player == 1)
                               {
                                   
                                   GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                       .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                       .GetComponent<humanPoint>().humanOn = false; 
                                   Player1Humans.Remove(GameObject.Find(item[0]));
                               }
                               else
                               {
                                   
                                   GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                       .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                       .GetComponent<humanPoint>().humanOn = false; 
                                   Player2Humans.Remove(GameObject.Find(item[0]));
                               }
                               
                               break;
                            case "pointB":
                                areaA.pointB.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointC":
                                areaA.pointC.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0]));
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointD":
                                areaA.pointD.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                        }

                        break;
                        
                        
                    case "AreaB":
                        switch (item[2])
                        {
                            case "pointA":
                                areaB.pointA.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0]));
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointB":
                                areaB.pointB.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointC":
                                areaB.pointC.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointD":
                                areaB.pointD.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                        }

                        break;
                    case "AreaC":
                        switch (item[2])
                        {
                            case "pointA":
                                areaC.pointA.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointB":
                                areaC.pointB.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false;
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointC":
                                areaC.pointC.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                            case "pointD":
                                areaC.pointD.GetComponent<point>().AttHumanList.Add(GameObject.Find(item[0])); 
                                if (player == 1)
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player2Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player2Humans.Remove(GameObject.Find(item[0]));
                                }
                                else
                                {
                                    
                                    GameObject.Find(item[0]).transform.parent.GetComponent<Barrack>()
                                        .humanPoints[Player1Humans.IndexOf(GameObject.Find(item[0]))]
                                        .GetComponent<humanPoint>().humanOn = false; 
                                    Player1Humans.Remove(GameObject.Find(item[0]));
                                }
                                break;
                        }
                        break;
                }
            }
            receiveBuff = null;
            temp2.Clear();
            return true;
        }

        return false;
    }

    private void countStart(bool ready)
    {
        if (ready)
        {
            countDown.GameObject().SetActive(true);
            ready = false;
        }
        else
        {
            return;
        }
    }
    
    IEnumerator holdSec()
    {
        countDown.GetComponent<CountDown>().waitMoving();
        yield return new WaitForSeconds(15f);
        doMapCheck();
        supplyChess(); 
        countDown.GameObject().SetActive(false);
        countDown.GameObject().SetActive(true);
    }
    
    

    
}
