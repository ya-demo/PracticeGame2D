using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMan : MonoBehaviour
{
    bool isAlive, isIdle, jumpAttack, isJump, isSlide, isHurt, canBeHurt;
    public int life;
    public float attackDistance, jumpHeight, jumpSpeed, downSpeed, slideSpeed;
    Animator myAnim;
    GameObject player;
    Vector3 slideTarget;
    BoxCollider2D myCollider;
    SpriteRenderer mySr;

    // Start is called before the first frame update
    void Awake()
    {
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        mySr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        isAlive = true;
        isIdle = true;
        isJump = true;
        isSlide = false;
        isHurt = false;
        jumpAttack = false;
        canBeHurt = true;
        life = 3;
        attackDistance = 2.5f;
        jumpHeight = 3.6f;
        jumpSpeed = 15f;
        downSpeed = 15f;
        slideSpeed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            if (isIdle)
            {
                LookAtPlayer();
                if(Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
                {
                    isIdle = false;
                    StartCoroutine("IdleToSlide");

                }else{
                    isIdle = false;
                    StartCoroutine("IdleToJump");
                }
            }else if(jumpAttack)
            {
                LookAtPlayer();
                if(isJump)
                {
                    Vector3 target = new Vector3(player.transform.position.x, jumpHeight, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, target, jumpSpeed * Time.deltaTime);
                    myAnim.SetBool("Jump", true);
                }else{
                    myAnim.SetBool("Jump", false);
                    myAnim.SetBool("Down", true);
                    Vector3 target = new Vector3(transform.position.x, -1.71f, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, target, downSpeed * Time.deltaTime);
                }

                if(transform.position.y == jumpHeight)
                {
                    isJump = false;
                }else if(transform.position.y == -1.71f){
                    jumpAttack = false;
                    StartCoroutine("JumpToIdle");
                }
            }else if(isSlide)
            {
                myAnim.SetBool("Slide", true);
                transform.position = Vector3.MoveTowards(transform.position, slideTarget, slideSpeed * Time.deltaTime);
                if(transform.position == slideTarget)
                {
                    myCollider.offset = new Vector2(0.0377214f, -0.1320254f);
                    myCollider.size = new Vector2(1.401899f, 2.071814f);
                    myAnim.SetBool("Slide", false);
                    isSlide = false;
                    isIdle = true;
                }
            }else if(isHurt)
            {
                Vector3 hurtTarget = new Vector3(transform.position.x, -1.71f, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, hurtTarget, downSpeed * Time.deltaTime);                
            }
        }else{
                Vector3 hurtTarget = new Vector3(transform.position.x, -1.71f, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, hurtTarget, downSpeed * Time.deltaTime);        
        }
    }

    void LookAtPlayer()
    {
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator IdleToJump()
    {
        yield return new WaitForSeconds(1f);
        jumpAttack = true;
    }
    IEnumerator IdleToSlide()
    {
        yield return new WaitForSeconds(1f);
        LookAtPlayer();
        myCollider.offset = new Vector2(0.042433684f, -0.4007912f);
        myCollider.size = new Vector2(1.562215f, 1.5334283f);
        slideTarget = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        isSlide = true;
    }

    IEnumerator JumpToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        isIdle = true;
        isJump = true;
        myAnim.SetBool("Jump", false);
        myAnim.SetBool("Down", false);
    }

    IEnumerator SetAnimHurtToFalse()
    {
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("Hurt", false);
        myAnim.SetBool("Jump", false);
        myAnim.SetBool("Down", false);
        myAnim.SetBool("Slide", false);
        isHurt = false;
        isIdle = true;
        mySr.material.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(2f);
        canBeHurt = true;
        mySr.material.color = new Color(1f, 1f, 1f, 1f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerAttack")
        {
            if(!canBeHurt)
                return;

            life--;
            if(life < 1)
            {
                isAlive = false;
                myCollider.enabled = false;
                StopAllCoroutines();
                myAnim.SetBool("Dead", true);
                myAnim.SetBool("Hurt", false);
            }else{
                isIdle = false;
                jumpAttack = false;
                isSlide = false;
                isHurt = true;
                StopCoroutine("JumpToIdle");
                StopCoroutine("IdleToSlide");
                StopCoroutine("IdleToJump");
                myAnim.SetBool("Hurt", true);
                StartCoroutine("SetAnimHurtToFalse");
            }
            canBeHurt = false;
        }
    }
}
