using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapMain : MonoBehaviour
{

    public Button startBtn;
    
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(btnClick);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void btnClick()
    {
        this.GameObject().SetActive(false);
    }

    
    
    

     

    
    


}
