using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    float hAxis;
    float vAxis;
    public float speed;
    Vector3 moveVec;
    bool wDown;
    bool jDown;
    bool isJump;
    bool isDamage;
    Animator anim;
    Rigidbody rigid;
    public int health;
    public bool isDie = false;

    void Start()
    {
        health = 100;
        
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            isJump = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
             isJump = false;
        }    
    }
    public void Hit()
    {
        this.health -= 10;
        if(this.health <= 0)
        {
            Die();
        }
        
    }
    public void Die()
    {
        if (this.isDie == false)
        {
            this.isDie = true;
            this.gameObject.SetActive(false);
        }
    }
}