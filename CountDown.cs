using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    TextMeshProUGUI t;
    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        t = GetComponent<TextMeshProUGUI>();
        t.enabled = true;
        coroutine = StartCountDown();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HangOn()
    {
        StopCoroutine(coroutine);
        t.text = "Wait for your opponent...";
    }

    public void waitMoving()
    {
        t.text = "Wait for soliders moving...";
    }

    IEnumerator StartCountDown()
    {
        t.text = "60...";
        for (int i = 59; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            t.text = i.ToString() + "...";
            if (i == 0)
            {
                //TurnTimeOut();
                yield break;
            }
        }
    }
}
