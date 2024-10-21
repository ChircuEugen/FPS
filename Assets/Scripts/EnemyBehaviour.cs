using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private GameObject fireBall;
    private NavMeshAgent agent;
    private Animator anim;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] float playerInRange;
    [SerializeField] float attackRange;
    private bool isPlayerInRange;
    private bool isAttackRange;

    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    private Vector3 currentPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentPoint = pointA; 
        agent.SetDestination(currentPoint);  
    }

    private void Update()
    {

        isPlayerInRange = Physics.CheckSphere(transform.position, playerInRange, playerLayer);
        isAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);


        if(!isPlayerInRange && !isAttackRange) Patrol();
        if(isPlayerInRange && !isAttackRange) FollowPlayer();
        if (isPlayerInRange && isAttackRange) AttackPlayer();

    }

    void Patrol()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            if (currentPoint == pointA) currentPoint = pointB;
            else currentPoint = pointA;
        }
        anim.SetBool("isRunning", true);
        agent.SetDestination(currentPoint);
    }

    void FollowPlayer()
    {
        anim.SetBool("isRunning", true);
        agent.SetDestination(playerPosition.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerPosition);
        anim.SetBool("isRunning", false);
        anim.SetTrigger("attack");
    }

    void CastFireball()
    {
        Instantiate(fireBall, fireBallSpawnPoint.position, fireBallSpawnPoint.rotation);
    }


}
