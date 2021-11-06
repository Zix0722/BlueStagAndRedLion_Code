using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMove : MonoBehaviour
{
    public Vector3 targetPos;

    private Vector3 startPos;

    public bool move = false;
    private Animator Ani;

    public bool moveDone = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        Ani = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Ani.SetBool("walk",true);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.1f);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                move = false;
                moveDone = true;
                Ani.SetBool("walk",false);
            }
        }
    }

    public void moveTo(Vector3 targetPos)
    {
        this.targetPos=targetPos;
        move = true;
    }
}
