using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class ZombieScript : MonoBehaviour
{

    [SerializeField] public int HP=100;
    private Animator animator;

    private NavMeshAgent navAgent;
    
    void Start()
    {
        animator= GetComponent<Animator>();
        navAgent=GetComponent<NavMeshAgent>();
        animator.SetBool("isAlive",true);
    }


    public void TakeDamage (int damageAmount)
    {
        HP-=damageAmount;

        if (HP<=0)
        {
            
            animator.SetTrigger("DIE");
            animator.SetBool("isAlive",false);
            Destroy(gameObject,4f);
        }

        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

  
}
