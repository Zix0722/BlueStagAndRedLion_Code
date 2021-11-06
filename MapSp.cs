using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapSp : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> ThereWaitHumanList;

    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;
    public int OccupiedBy = 0;
    [HideInInspector]
    //public List<GameObject> AttHumanList;

    int InHumanNum = 0;

    enum ThereHumanType
    {
        None,
        P1,
        P2
    }

    ThereHumanType ThereType = ThereHumanType.None;

    int ThereP1Num;
    int ThereP2Num;
    // Start is called before the first frame update
    void Start()
    {
        //AttHumanList = new List<GameObject>();
        ThereWaitHumanList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool compareTwo(string Man, string otherMan)
    {
        if (Man == "Y")
        {
            switch (otherMan)
            {
                case "O":
                    return true;
                default:
                    return false;
            }
        }
        if (Man == "@")
        {
            switch (otherMan)
            {
                case "Y":
                    return true;
                default:
                    return false;
            }
        }
        if (Man == "O")
        {
            switch (otherMan)
            {
                case "@":
                    return true;
                default:
                    return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void doCheck()
    {
        pointA.GetComponent<point>().Check();
        pointB.GetComponent<point>().Check();
        pointC.GetComponent<point>().Check();
        pointD.GetComponent<point>().Check();
    }
    public void MapCheck()
    {
        doCheck();
        if (pointA.GetComponent<point>().hasManIn && pointB.GetComponent<point>().hasManIn)
        {
            if (!pointA.GetComponent<point>().AttHumanList[0]
                .CompareTag(pointA.GetComponent<point>().AttHumanList[0].tag))
            {
               if (compareTwo(pointA.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag,
                             pointB.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag))
                         {
                             pointB.GetComponent<point>().AttHumanList[0].GameObject().SetActive(false);
                         } 
            }
        }

        if (pointB.GetComponent<point>().hasManIn && pointC.GetComponent<point>().hasManIn)
        {
            if (!pointB.GetComponent<point>().AttHumanList[0]
                .CompareTag(pointC.GetComponent<point>().AttHumanList[0].tag))
            {
               if (compareTwo(pointB.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag,
                                      pointC.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag))
                                  {
                                      pointC.GetComponent<point>().AttHumanList[0].GameObject().SetActive(false);
                                  }  
            }
        }

        if (pointC.GetComponent<point>().hasManIn && pointD.GetComponent<point>().hasManIn)
        {
            if (!pointC.GetComponent<point>().AttHumanList[0]
                .CompareTag(pointD.GetComponent<point>().AttHumanList[0].tag))
            {
              if (compareTwo(pointC.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag,
                                     pointD.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag))
                                 {
                                     pointD.GetComponent<point>().AttHumanList[0].GameObject().SetActive(false);
                                 }   
            }
        }

        if (pointD.GetComponent<point>().hasManIn && pointA.GetComponent<point>().hasManIn)
        {
            if (!pointA.GetComponent<point>().AttHumanList[0]
                .CompareTag(pointA.GetComponent<point>().AttHumanList[0].tag))
            {
               if (compareTwo(pointD.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag,
                                     pointA.GetComponent<point>().AttHumanList[0].transform.GetChild(0).tag))
                                 {
                                     pointA.GetComponent<point>().AttHumanList[0].GameObject().SetActive(false);
                                 }  
            }
        }
        
        
        /*foreach (var item in ThereWaitHumanList)
        {
            AttHumanList.Add(item);
        }
        ThereWaitHumanList.Clear();
        //Debug.Log(AttHumanList.Count);

        InHumanNum = AttHumanList.Count;*/
        
        /*for (int i = 0; i < pointA.GetComponent<point>().AttHumanList.Count; i++)
        {
             StartCoroutine(HumanMoveIn(pointA.GetComponent<point>().AttHumanList[i], new Vector3(pointA.transform.position.x + (int)(i%3),
                     pointA.transform.position.y+3,
                     pointA.transform.position.z + (int)(i/3))
                 )
             );
        }
        for (int i = 0; i < pointB.GetComponent<point>().AttHumanList.Count; i++)
        {
            StartCoroutine(HumanMoveIn(pointB.GetComponent<point>().AttHumanList[i], new Vector3(pointB.transform.position.x + (int)(i%3),
                    pointB.transform.position.y+3,
                    pointB.transform.position.z + (int)(i/3))
                )
            );
        }
        for (int i = 0; i < pointC.GetComponent<point>().AttHumanList.Count; i++)
        {
            StartCoroutine(HumanMoveIn(pointC.GetComponent<point>().AttHumanList[i], new Vector3(pointC.transform.position.x + (int)(i%3),
                    pointC.transform.position.y+3,
                    pointC.transform.position.z + (int)(i/3))
                )
            );
        }
        for (int i = 0; i < pointD.GetComponent<point>().AttHumanList.Count; i++)
        {
            StartCoroutine(HumanMoveIn(pointD.GetComponent<point>().AttHumanList[i], new Vector3(pointD.transform.position.x + (int)(i%3),
                    pointD.transform.position.y+3,
                    pointD.transform.position.z + (int)(i/3))
                )
            );
        }*/
        
        confirmStutas();
    }

    void confirmStutas()
    {
        int cnt = 0;
        if (pointA.GetComponent<point>().hasManIn)
        {
            if (pointA.GetComponent<point>().AttHumanList[0].tag == "P1")
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }
        
        if (pointB.GetComponent<point>().hasManIn)
        {
            if (pointB.GetComponent<point>().AttHumanList[0].tag == "P1")
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }
        
        if (pointC.GetComponent<point>().hasManIn)
        {
            if (pointC.GetComponent<point>().AttHumanList[0].tag == "P1")
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }
        
        if (pointD.GetComponent<point>().hasManIn)
        {
            if (pointD.GetComponent<point>().AttHumanList[0].tag == "P1")
            {
                cnt++;
            }
            else
            {
                cnt--;
            }
        }

        if (cnt > 0)
        {
            OccupiedBy = 1;
        }
        else if (cnt < 0)
        {
            OccupiedBy = 2;
        }else if (cnt == 0)
        {
            OccupiedBy = 0;
        }
    }

    /*void AttackEvent()
    {
        int P1Num = 0;
        int P2Num = 0;

        foreach (var item in AttHumanList)
        {
            if (item.gameObject.tag == "P1")
            {
                P1Num++;
                //Debug.LogWarning(P1Num);
            }
            if (item.gameObject.tag == "P2")
            {
                P2Num++;
            }
        }
        int tempP2 = P2Num;
        int tempP1 = P1Num;

        if (Mathf.Abs(P1Num - P2Num) != 0)
        {
            if (P1Num > P2Num)
            {
                
                P1Num -= P2Num;
                P2Num = 0;
                ThereType = ThereHumanType.P1;
            }
            else if (P2Num > P1Num)
            {
                P2Num -= P1Num;
                P1Num = 0;
                ThereType = ThereHumanType.P2;
            }

            int total = AttHumanList.Count;
            
                if (P1Num > 0)
                {
                    for (int j = 0; j < tempP2; j++)
                    {
                        for (int z = 0; z < AttHumanList.Count; z++)
                        {
                           if (AttHumanList[z].gameObject.tag == "P1")
                           {
                               
                               AttHumanList[z].SetActive(false);
                               AttHumanList.Remove(AttHumanList[z]);
                               break;
                           }
                        }
                        for (int z = 0; z < AttHumanList.Count; z++)
                        {
                            if (AttHumanList[z].gameObject.tag == "P2")
                            {
                                
                                AttHumanList[z].SetActive(false);
                                AttHumanList.Remove(AttHumanList[z]);
                                break;
                            }
                        }
                        
                    }
                }
                if (P2Num > 0)
                {
                    for (int j = 0; j < tempP1; j++)
                    {
                        for (int z = 0; z < AttHumanList.Count; z++)
                        {
                            if (AttHumanList[z].gameObject.tag == "P1")
                            {
                                AttHumanList[z].SetActive(false);
                                AttHumanList.Remove(AttHumanList[z]);
                                break;
                            }
                        }
                        for (int z = 0; z < AttHumanList.Count; z++)
                        {
                            if (AttHumanList[z].gameObject.tag == "P2")
                            {
                                AttHumanList[z].SetActive(false);
                                AttHumanList.Remove(AttHumanList[z]);
                                break;
                            }
                        }
                        
                    }
                }

                foreach (var item in AttHumanList)
            {
                ThereWaitHumanList.Add(item);
                //Debug.LogWarning(ThereWaitHumanList.Count+"W");
            }
        }
        else
        {
            Debug.Log(AttHumanList.Count);
            ThereType = ThereHumanType.None;
            for (int i = 0; i < AttHumanList.Count; i++)
            {
                AttHumanList[i].SetActive(false);
            }
        }

        AttHumanList.Clear();

        switch (ThereType)
        {
            case ThereHumanType.None:
                ThereP1Num = 4;
                ThereP2Num = 4;
                break;
            case ThereHumanType.P1:
                ThereP1Num = 4 - ThereWaitHumanList.Count;
                ThereP2Num = 4;
                break;
            case ThereHumanType.P2:
                ThereP2Num = 4 - ThereWaitHumanList.Count;
                ThereP1Num = 4;
                break;
            default:
                break;
        }
    }*/

    IEnumerator HumanMoveIn(GameObject mObj, Vector3 targetPos)
    {
        yield return new WaitForSeconds(0.01f);
        float maxDistanceDelta = 0.0005f;
        while (true)
        {
            if (Vector3.Distance(mObj.transform.position, targetPos) > 0.1f)
            {
                //mObj.transform.position = Vector3.Lerp(mObj.transform.position, targetPos, 0.000001f);
                //mObj.transform.Translate(Vector3.forward*5*Time.deltaTime,Space.Self);
                mObj.transform.position = Vector3.MoveTowards(mObj.transform.position, targetPos, maxDistanceDelta);
                
            }
            else
            {
                mObj.transform.position = targetPos;
                break;
            }
        }

        InHumanNum--;
        if (InHumanNum <= 0)
        {
            //AttackEvent();
        }
    }

    void HumanMove(GameObject mObj, Vector3 targetPos)
    {
        mObj.GetComponent<HumanMove>().moveTo(targetPos);
    }

    public void allMove()
    {
        for (int i = 0; i < pointA.GetComponent<point>().AttHumanList.Count; i++)
        {
            HumanMove(pointA.GetComponent<point>().AttHumanList[i], new Vector3(pointA.transform.position.x + (int)(i%3),
                pointA.transform.position.y+6,
                pointA.transform.position.z + (int)(i/3))
            );
        }
        for (int i = 0; i < pointB.GetComponent<point>().AttHumanList.Count; i++)
        {
            HumanMove(pointB.GetComponent<point>().AttHumanList[i], new Vector3(pointB.transform.position.x + (int)(i%3),
                pointB.transform.position.y+6,
                pointB.transform.position.z + (int)(i/3))
            );
            
        }
        for (int i = 0; i < pointC.GetComponent<point>().AttHumanList.Count; i++)
        {
            HumanMove(pointC.GetComponent<point>().AttHumanList[i], new Vector3(
                pointC.transform.position.x + (int)(i % 3),
                pointC.transform.position.y + 6,
                pointC.transform.position.z + (int)(i / 3))
            );

        }
        for (int i = 0; i < pointD.GetComponent<point>().AttHumanList.Count; i++)
        {
            HumanMove(pointD.GetComponent<point>().AttHumanList[i], new Vector3(
                pointD.transform.position.x + (int)(i % 3),
                pointD.transform.position.y + 6,
                pointD.transform.position.z + (int)(i / 3))
            );

        }
    }
}
