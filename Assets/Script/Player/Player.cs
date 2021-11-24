using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool isJumpPressed, isAttack, canJump, isHurt, canBeHurt;
    [HideInInspector]
    public int playerLife;
    [HideInInspector]
    public int playerKunai;
    [HideInInspector]
    public int playerStone;
    public float mySpeed;
    public float jumpForce;
    [HideInInspector]
    public Animator myAnim;
    Rigidbody2D myRigi;
    SpriteRenderer mySr;

    public AudioClip[] myAudioClips;
    AudioSource myAudio;

    Canvas myCanvas;
    public GameObject attackCollider;
    public GameObject kunaiPrefab;

    private float kunaiDistance;

    private void Awake()
    {
        playerLife = PlayerPrefs.GetInt("PlayerLife");
        playerKunai = PlayerPrefs.GetInt("PlayerKunai");
        playerStone = PlayerPrefs.GetInt("PlayerStone");
        mySpeed = 5;
        jumpForce = 20;
        isJumpPressed = false;
        isAttack = false;
        isHurt = false;
        canJump = true;
        canBeHurt = true;
        myAudio = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        mySr = GetComponent<SpriteRenderer>();
        myCanvas = GameObject.Find("/Canvas").GetComponent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canJump == true && isHurt == false)
        {
            isJumpPressed = true;
            canJump = false;
        }

        if(Input.GetKeyDown(KeyCode.T) && isHurt == false)
        {
            myAnim.SetTrigger("Attack");
            isAttack = true;
            canJump = false;
        }
        if(Input.GetKeyDown(KeyCode.G) && isHurt == false 
        && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Throw") 
        && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if(playerKunai > 0)
            {
                playerKunai --;
                PlayerPrefs.SetInt("PlayerKunai", playerKunai);
                myCanvas.KunaiUpdate();
                myAnim.SetTrigger("Throw");
                isAttack = true;
                canJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // float keyAxis = Input.GetAxis("Horizontal");
        float keyAxisLR = Input.GetAxisRaw("Horizontal");
        if(isAttack == true || isHurt == true)
        {
            keyAxisLR = 0;
        }

        // float keyAxisUD = Input.GetAxisRaw("Vertical");
        if(keyAxisLR > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if(keyAxisLR < 0){
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        myAnim.SetFloat("Run", Mathf.Abs(keyAxisLR));

        if(isJumpPressed)
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
            myAnim.SetBool("Jump", true);
        }
        if(!isHurt)
        {
            myRigi.velocity = new Vector2(keyAxisLR * mySpeed, myRigi.velocity.y);
        }

        // if(keyAxisLR != 0 && keyAxisUD == 0)
        // {
        //     myAnim.SetFloat("Run", Mathf.Abs(keyAxisLR));
        // }else if(Mathf.Abs(keyAxisUD) > 0.1f && keyAxisLR == 0)
        // {
        //     myAnim.SetFloat("Run", Mathf.Abs(keyAxisUD));
        // }else if(keyAxisUD != 0 && keyAxisLR != 0)
        // {
        //     myAnim.SetFloat("Run", Mathf.Abs(keyAxisLR));
        // }else{
        //     myAnim.SetFloat("Run", 0);
        // }

        // // 依照 Animator 中取的名字來設定\
        // var tmpX = myRigi.position.x + keyAxisLR * Time.fixedDeltaTime * mySpeed;
        // var tmpY = myRigi.position.y + keyAxisUD * Time.fixedDeltaTime * mySpeed;
        // myRigi.position = new Vector2(tmpX, tmpY);
    }
    
    //若被攻擊會停止攻擊 有可能不會執行到
    public void SetIsAttackFalse()
    {
        isAttack = false;
        canJump = true;
        myAnim.ResetTrigger("Attack");
        myAnim.ResetTrigger("Throw");
    }

    public void ForIsHurtSetting()
    {
        isAttack = false;
        myAnim.ResetTrigger("Attack");
        myAnim.ResetTrigger("Throw");
        SetAttackColliderOff();
    }

    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }

    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    public void InstantiateKunai()
    {
            if(transform.localScale.x == 1)
            {
                kunaiDistance = 1.0f;
            }else if(transform.localScale.x == -1)
            {
                kunaiDistance = -1.0f;
            }
            Vector3 tmp = new Vector3(transform.position.x + kunaiDistance, transform.position.y, transform.position.z);
            Quaternion tmpRotation = Quaternion.Euler(0f, 0f, -90f); //Quaternion.identity 預設
            Instantiate(kunaiPrefab, tmp, tmpRotation);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" && isHurt == false && canBeHurt == true)
        {
            myAudio.PlayOneShot(myAudioClips[0]);
            playerLife--;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            myCanvas.LifeUpdate();
            if(playerLife >= 1)
            {
                isHurt = true;
                canBeHurt = false;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("Hurt", true);
                if(transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 8.0f);
                }else if(transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 8.0f);
                }

                StartCoroutine("SetIsHurtFalse");
            }else if(playerLife < 1)
            {
                isHurt = true;
                myRigi.velocity = new Vector2(0f, 0f);
                myAnim.SetBool("Dead", true);
                PlayerPrefs.SetInt("PlayerLife", 3);
                FadeInOut.instance.SceneFadeInOut("SelectLevel");
            }
        }
        if(collider.tag == "Item")
        {
            myAudio.PlayOneShot(myAudioClips[1]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Enemy" && isHurt == false && canBeHurt == true)
        {
            myAudio.PlayOneShot(myAudioClips[0]);
            playerLife--;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            myCanvas.LifeUpdate();
            if(playerLife >= 1)
            {
                isHurt = true;
                canBeHurt = false;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("Hurt", true);
                if(transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.0f, 8.0f);
                }else if(transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.0f, 8.0f);
                }

                StartCoroutine("SetIsHurtFalse");
            }else if(playerLife < 1)
            {
                isHurt = true;
                myRigi.velocity = new Vector2(0f, 0f);
                myAnim.SetBool("Dead", true);
                PlayerPrefs.SetInt("PlayerLife", 3);
                FadeInOut.instance.SceneFadeInOut("SelectLevel");

            }
        }
        if(collider.tag == "Item")
        {
            myAudio.PlayOneShot(myAudioClips[1]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "BoundBottom")
        {
            playerLife = 0;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            myCanvas.LifeUpdate();
            PlayerPrefs.SetInt("PlayerLife", 3);

            isHurt = true;
            myRigi.velocity = new Vector2(0f, 0f);
            myAnim.SetBool("Dead", true);
            myAudio.PlayOneShot(myAudioClips[4]);

            FadeInOut.instance.SceneFadeInOut("SelectLevel");
        }
    }

    IEnumerator SetIsHurtFalse()
    {
        yield return new WaitForSeconds(1.0f);
        myAnim.SetBool("Hurt", false);
        isHurt = false;
        
        yield return new WaitForSeconds(1.0f);
        canBeHurt = true;
        mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 1.0f);
    }

    public void PlaySwordEffect()
    {
        myAudio.PlayOneShot(myAudioClips[3]);
    }

    public void PlayKunaiEffect()
    {
        myAudio.PlayOneShot(myAudioClips[2]);
    }
}

