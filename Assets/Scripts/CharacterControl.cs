using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//角色状态
public enum CHARACTER_STATE
{
    GROUND,
    AIR
}
public class CharacterControl : MonoBehaviour
{
    public Transform mTrans;
    public Rigidbody2D mRigid2D;
    public Animator anim2D;
    public CHARACTER_STATE characterState;

    //玩家输入
    public string pMove = "Horizontal";
    public string pJump = "Jump";
    public string pAttack = "x";


    //处理
    float moving;
    float jumping;
    bool ismoving;
    bool isjumpingInput = false;//按下跳跃键？

    //角色属性
    public float moveSpeed = 1;
    public float jumpForce = 1;

    // Start is called before the first frame update
    void Start()
    {
        mTrans = this.GetComponent<Transform>();
        mRigid2D = this.GetComponent<Rigidbody2D>();
        anim2D = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCalculation();
        CanJump();
        //Debug.Log("moving: " + moving);

        //设置角色状态 todo

        //根据角色状态 todo
        switch(characterState)
        {
            case CHARACTER_STATE.GROUND:
                break;

            case CHARACTER_STATE.AIR:
                break;
        }
    }

    //水平输入，判断是否可以进行移动；方向、移动
    void MoveCalculation()
    {
        moving = Input.GetAxis(pMove);
        if (moving != 0)
        {
            ismoving = true;
            mTrans.right = (Vector2.right * moving).normalized;
            anim2D.SetBool("isRunning", true);
        } else
        {
            ismoving = false;
            anim2D.SetBool("isRunning", false);
        }
        if (ismoving)
        {
            Vector2 tempVec = new Vector2(0, mRigid2D.velocity.y);
            mRigid2D.velocity = new Vector2(moving, 0).normalized * moveSpeed + tempVec;
        }
    }

    //判断是否能跳跃的方法
    void CanJump()
    {
        isjumpingInput = Input.GetButtonDown(pJump);
        if (isjumpingInput)
        {
            Debug.Log("跳跃");
            mRigid2D.AddForce(Vector2.up.normalized * jumpForce);
        }
    }

    
}
