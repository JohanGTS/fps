using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehaviour : StateMachineBehaviour
{
   
    float timer;
    public float patrolingTimer=10f;
    public float detectionArea=18f;
    public float patrolSpeed= 2f;
    Transform player;
    NavMeshAgent agent;

    List<Transform> wayPointList = new  List <Transform> ();
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       agent= animator.GetComponent<NavMeshAgent>();
       agent.speed	= patrolSpeed;
       timer=0;
       GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");

       foreach (Transform t in waypointCluster.transform)
       {
        wayPointList.Add(t);
       }
       Vector3 nextPostion = wayPointList[Random.Range(0,wayPointList.Count)].position;
       agent.SetDestination(nextPostion);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (agent.remainingDistance <= agent.stoppingDistance)
       {
        agent.SetDestination(wayPointList[Random.Range(0,wayPointList.Count)].position);
       }

       timer+= Time.deltaTime;
       if (timer> patrolingTimer)
       {
        animator.SetBool("isPatrolling",false);
       }

       float distanceFromPlayer= Vector3.Distance(player.position,animator.transform.position);
       if (distanceFromPlayer< detectionArea)
       {
        animator.SetBool("isChasing",true);
       }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(agent.transform.position);
    }
}
