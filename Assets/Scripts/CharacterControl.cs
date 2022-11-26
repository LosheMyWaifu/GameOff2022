using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//角色状态
public enum CHARACTER_STATE
{
    IDLE,
    RUN,
    JUMP,
    FALL,
    ATTACK,
}
public class CharacterControl : MonoBehaviour
{
    public Transform mTrans;
    public Rigidbody2D mRigid2D;
    public Animator anim2D;
    public new CapsuleCollider2D collider2D;

    public CHARACTER_STATE characterState;

    //玩家输入
    public string pMove = "Horizontal";
    public string pJump = "Jump";
    public string pAttack = "x";


    //处理
    float movement;
    float jumping;
    bool ismoving;
    bool isjumpingInput = false;//按下跳跃键？


    public bool isGround;

    //角色属性
    public float moveSpeed = 1;
    public float jumpForce = 1;
    public float fallthrehold = 1;

    Collider2D[] results = new Collider2D[2];
    ContactFilter2D contactF;
    // Start is called before the first frame update
    void Start()
    {
        mTrans = this.GetComponent<Transform>();
        mRigid2D = this.GetComponent<Rigidbody2D>();
        anim2D = this.GetComponentInChildren<Animator>();
        collider2D = this.GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        RunMethed();
        JumpMethed();
        SwitchAnimation();
        AttackMethed();
    }

    private void FixedUpdate()
    {
         Vector2 tempVec = new Vector2(0, mRigid2D.velocity.y);
         mRigid2D.velocity = new Vector2(movement, 0).normalized * moveSpeed*Time.deltaTime + tempVec;//移动        
    }
    void RunMethed()
    {
        anim2D.SetBool("isMoving", false);
        movement = Input.GetAxis(pMove); //获取键盘输入
        if (movement != 0)
        {
            mTrans.right = (Vector2.right * movement).normalized; //根据输入值转向
            if(isGround)
            {
                anim2D.SetBool("isMoving", true);
            }
            
        }
    }

    void JumpMethed()
    {
        //anim2D.SetBool("isJumping", false);
        //anim2D.SetBool("isFalling", false);
        isjumpingInput = Input.GetButtonDown(pJump);
        if (isjumpingInput && isGround)
        {
            mRigid2D.AddForce(Vector2.up.normalized * jumpForce);
            anim2D.SetBool("isJumping", true);
        }
    }

    void SwitchAnimation()
    {
        anim2D.SetBool("isIdle", false);
        if (anim2D.GetBool("isJumping"))
        {
            if(mRigid2D.velocity.y < -fallthrehold)
            {
                anim2D.SetBool("isJumping", false);
                anim2D.SetBool("isFalling", true);
            }
        }else if(isGround)
        {
            anim2D.SetBool("isFalling", false);
            if(!anim2D.GetBool("isMoving"))
            {
                anim2D.SetBool("isIdle", true);
            }

        }
    }

    void AttackMethed ()
    {
        //anim2D.SetBool("isAttacking", false);
        if (Input.GetKeyDown(pAttack))
        {
            //todo
            anim2D.SetTrigger("isAttacking");
        }

    }

    void CheckGround()
    {
        if(collider2D.IsTouchingLayers(LayerMask.GetMask("Ground") ))
        {
            isGround= true; 
        }else
        {
            isGround= false;
        }
        //Debug.Log(isGround);
    }  
   //
}
