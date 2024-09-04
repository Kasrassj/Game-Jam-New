using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public float attackRange;
    public bool playerInAttackRange;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
        agent.destination = player.position;
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange);
    }

    private void Attack()
    {

    }
}
