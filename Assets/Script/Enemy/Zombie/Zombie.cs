using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Vector3 originPosition;
    public Vector3 targetPosition;
    public Vector3 turnPoint;
    public int life;
    public float mySpeed;
    protected bool isFirstTime, isAttackCheck; 
    protected Animator myAnim;
    protected BoxCollider2D myCollider;
    protected SpriteRenderer mySr;
    protected GameObject player;
    public GameObject attackCollider;

    [SerializeField]
    protected AudioClip[] myAudioClips;
    protected AudioSource myAudio;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        isFirstTime = true;
        isAttackCheck = false;
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        mySr = GetComponent<SpriteRenderer>();
        myAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndAttack();
    }

    protected virtual void MoveAndAttack()
    {
        if(life < 1)    
        {
            return;
        }

        if(Vector3.Distance(player.transform.position, transform.position) < 1.2f)
        {
            if(player.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }else {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || myAnim.GetCurrentAnimatorStateInfo(0).IsName("WaitAttack"))
            {
                return;
            }
            myAudio.PlayOneShot(myAudioClips[1]);
            myAnim.SetTrigger("Attack");
            isAttackCheck = true;
            return;
        }else{
            if(isAttackCheck)
            {
                if(turnPoint == targetPosition)
                {
                    StartCoroutine(Turn(false));
                }else{                    
                    StartCoroutine(Turn(true));
                }
                isAttackCheck = false;
            }
        }

        if(transform.position.x == targetPosition.x)
        {
            myAnim.SetTrigger("Idel");
            turnPoint = originPosition;
            StartCoroutine(Turn(true));
            isFirstTime = false;
        } 
        else if(transform.position.x == originPosition.x)
        {
            if(!isFirstTime)
            {
                myAnim.SetTrigger("Idel");
            }
            turnPoint = targetPosition;
            StartCoroutine(Turn(false));
        }
        if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Work"))
        {
            transform.position = Vector3.MoveTowards(transform.position, turnPoint, mySpeed * Time.deltaTime);
        }
    }

    protected IEnumerator Turn(bool isRight)
    {
        yield return new WaitForSeconds(1.5f);
        if(isRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }else{
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }

    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerAttack")
        {
            myAudio.PlayOneShot(myAudioClips[0]);
            life--;
            if(life > 0)
            {
                myAnim.SetTrigger("Hurt");
            }else if(life < 1)
            {
                myCollider.enabled = false;
                myAnim.SetTrigger("Dead");
                StartCoroutine("AfterDead");
            }
        }
    }

    IEnumerator AfterDead()
    {
        yield return new WaitForSeconds(1.0f);
        //寫 1 代表原本的顏色
        mySr.material.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(1.0f);
        mySr.material.color = new Color(1f, 1f, 1f, 0.2f);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
