using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warning : MonoBehaviour
{

    public static warning Instance { set; get; }
    public Button closeBtn;

    public Text content;
    
    
    // Start is called before the first frame update
    void Start()
    {
        closeBtn.onClick.AddListener(onBtnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onBtnClick()
    {
        this.gameObject.SetActive(false);
    }

    public void setText(string str)
    {
        content.text = str;
    }
}
