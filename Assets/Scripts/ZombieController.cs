using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    // --- AI Settings ---
    public NavMeshAgent agent;
    private Transform building; //The Objective
    public LayerMask whatIsBuilding, whatIsGround;
    public GameObject objective;

    public float timeBetweenAttacks; //If coroutine doesn't work we use this
    bool alreadyAttacked;

    public float attackRange = 10f; //distance for when to attack the building
    public bool buildingInAttackRange;
    
    
    private Rigidbody rb; //rigidbody

    public GameObject spawner;
    
    
    [SerializeField] 
    private float moveSpeed; //move speed
    [SerializeField]
    private int attackDamage; //attack damage
    
    
    public delegate void EnemyKilled(); //spawner on death
    public static event EnemyKilled OnEnemyKilled; //spawner on death
    
    
    [SerializeField] private int startingHealth = 5; //health
    private int currentHealth; //current health
    private Animator anim; //animator
    private Vector3 kek = new Vector3(0, 2, 0);


    private void Awake()
    {
        spawner = GameObject.Find("ZombieSpawner");
        objective = GameObject.Find( "SafeHouse");
        anim = GetComponentInChildren<Animator>(); //pick animator
        building = GameObject.Find("House").transform; //pick the building's transform
        rb = GetComponent<Rigidbody>(); //pick rigidbody
        agent = GetComponentInChildren<NavMeshAgent>();
    }
    

    private void OnEnable()
    {
        currentHealth = startingHealth; //set hp
    }

    void Update()
    {
        
        buildingInAttackRange = Physics.CheckSphere(transform.position+kek, attackRange, whatIsBuilding);
        

        if (!buildingInAttackRange)
        {
            Chase();
        }if(buildingInAttackRange){
            Attack();
            
        }

    }

    private void Chase()
    {
        anim.SetBool("Walking", true);
        agent.SetDestination(building.position);
    }
    private void Attack() // --- Attack ---
    {
        anim.SetBool("Walking",false); //set walking animation false
        agent.SetDestination(transform.position);
        transform.LookAt(building);

        if (!alreadyAttacked)
        {
            anim.SetTrigger("Attack");
            objective.GetComponent<ObjectiveHealth>().TakeDamage(attackDamage);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    } void ResetAttack()
    {
        alreadyAttacked = false;
    }

    
    public void TakeDamage(int damageAmount) //code for taking damage
    {
        currentHealth -= damageAmount;
        anim.SetTrigger("GetHit"); //animation for taking damage

        if (currentHealth <= 0)
        {
            Die(); //death when health < zero
        }
    }
 
    private void Die()
    {
        spawner.GetComponent<ZombieSpawner>().zombieCount++;
        StartCoroutine(Death());
        OnEnemyKilled();
    }

    IEnumerator Death() //Death Coroutine
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(.8f);
        gameObject.SetActive(false);
    }

    IEnumerator AttackCoroutine() //Attacking the building
    {
        anim.SetTrigger("Attack");
        objective.GetComponent<ObjectiveHealth>().TakeDamage(attackDamage);
        yield return new WaitForSeconds(6f);
    }
}
