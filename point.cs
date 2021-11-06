using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class point : MonoBehaviour
{
    public List<GameObject> AttHumanList;

    public bool hasManIn = false;
    // Start is called before the first frame update
    void Start()
    {
        AttHumanList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (AttHumanList.Count == 0)
        {
            return;
        }
        if (AttHumanList.Count == 1)
        {
            hasManIn = true;
            return;
        }
        if (AttHumanList[0].transform.GetChild(0).CompareTag(AttHumanList[1].transform.GetChild(0).tag))
        {
            AttHumanList[0].SetActive(false);
            AttHumanList[1].SetActive(false);
            AttHumanList.Clear();
        }
        else
        {
            if (AttHumanList[0].transform.GetChild(0).tag == "Y")
            {
                if (AttHumanList[1].transform.GetChild(0).tag == "@")
                {
                    AttHumanList[0].SetActive(false);
                    AttHumanList.Remove(AttHumanList[0]);
                    hasManIn = true;
                }
                else
                {
                    AttHumanList[1].SetActive(false);
                    AttHumanList.Remove(AttHumanList[1]);
                    hasManIn = true;
                }
            }
            
            if (AttHumanList[0].transform.GetChild(0).tag == "@")
            {
                if (AttHumanList[1].transform.GetChild(0).tag == "O")
                {
                    AttHumanList[0].SetActive(false);
                    AttHumanList.Remove(AttHumanList[0]);
                    hasManIn = true;
                }
                else
                {
                    AttHumanList[1].SetActive(false);
                    AttHumanList.Remove(AttHumanList[1]);
                    hasManIn = true;
                }
            }
            
            if (AttHumanList[0].transform.GetChild(0).tag == "O")
            {
                if (AttHumanList[1].transform.GetChild(0).tag == "Y")
                {
                    AttHumanList[0].SetActive(false);
                    AttHumanList.Remove(AttHumanList[0]);
                    hasManIn = true;
                }
                else
                {
                    AttHumanList[1].SetActive(false);
                    AttHumanList.Remove(AttHumanList[1]);
                    hasManIn = true;
                }
            }
        }
    }
}
