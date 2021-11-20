using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGirl : Zombie
{
    public float RunSpeed;

    
    protected override void MoveAndAttack()
    {
         if(life < 1)    
        {
            return;
        }

        if(Vector3.Distance(player.transform.position, transform.position) < 4f)
        {
            if(player.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }else {
                transform.localScale = new Vector3(1, 1, 1);
            }

            Vector3 newTarget = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            if(myAnim.GetCurrentAnimatorStateInfo(0).IsName("Work"))
            {
                transform.position = Vector3.MoveTowards(transform.position, newTarget, RunSpeed * Time.deltaTime);
            }

            isAttackCheck = true;
            return;
        }else{
            if(isAttackCheck)
            {
                if(transform.position.x > turnPoint.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }else if(transform.position.x < turnPoint.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }else{
                    if(turnPoint == targetPosition)
                    {
                        StartCoroutine(Turn(false));
                    }else{                    
                        StartCoroutine(Turn(true));
                    }
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
}
