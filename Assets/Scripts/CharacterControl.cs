using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Transform mTrans;
    public Rigidbody2D mRigid2D;
    public Animator anim2D;

    //玩家输入
    public string pMove = "Horizontal";
    public string pJump = "Space";
    public string pAttack = "X";


    //处理
    float moving;
    bool ismoving;

    //角色属性
    public float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        mTrans = this.GetComponent<Transform>();
        mRigid2D= this.GetComponent<Rigidbody2D>(); 
        anim2D = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCalculation();
        
    }

    private void FixedUpdate()
    {
        MoveImplement();
    }

    void MoveCalculation()
    {  
        moving = Input.GetAxis(pMove);
        if(moving!=0)
        {
            ismoving = true;
            mTrans.right = Vector2.right.normalized * moving;
            anim2D.SetBool("isRunning", true);
        }else
        {
           ismoving = false;
            anim2D.SetBool("isRunning", false);
        }        
    }

    void MoveImplement()
    {
        if(ismoving)
        {
            mRigid2D.velocity = new Vector2(moving, 0).normalized * moveSpeed;
        }
    }
}
