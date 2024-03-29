using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MutantChaseState : StateMachineBehaviour
{

NavMeshAgent agent;
Transform player;

public float chasespeed = 6f;
public float attackingDistance =2.5f;

public float stopChasingDistance =8f;



        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
              player = GameObject.FindGameObjectWithTag("Player").transform;
       agent= animator.GetComponent<NavMeshAgent>();
       agent.speed=chasespeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(player.position);
       animator.transform.LookAt(player);

       float distanceFromPlayer = Vector3.Distance(player.position,animator.transform.position);

       if (distanceFromPlayer> stopChasingDistance)
       {
        animator.SetBool("isChasing",false);
       }

       if (distanceFromPlayer< attackingDistance) 
       animator.SetBool("isAttacking",true);
       
      }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }
}
