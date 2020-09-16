using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Tooltip("移動速度"), Range(0, 100)]
    public float speed = 3f;
    [Tooltip("攻擊力"), Range(0, 100)]
    public float attack = 20f;
    [Tooltip("CD"), Range(0, 10)]
    public float cooldown = 3f;
    [Tooltip("血量"), Range(0, 1000)]
    public float hp = 350f;
    [Tooltip("經驗值"), Range(0, 1000)]
    public float exp = 30f;
    [Tooltip("掉寶率"), Range(0, 1f)]
    public float dropPer = 0.3f;
    [Tooltip("掉落寶物")]
    public Transform dropItem;
    public GameObject attackArea;

    public float trackDist = 8f;
    public float attackDist = 2.5f;


    private Vector3 origPos;     
    private NavMeshAgent nma;
    private Animator ani;
    private PlayerControl player;
    private float timer = 0;

    bool isTracking = false;
    IEnumerator RandWalk()
    {
        while(true)
        {
            if (!isTracking && nma.remainingDistance <= attackDist)
            {
                yield return new WaitForSeconds(Random.Range(2, 4));
                float range = trackDist / 2 + attackDist;
                nma.SetDestination(origPos + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)));
            }
            else
            {
                yield return null;
            }
        }
    }
    void Move()
    {
        ani.SetFloat("move", nma.velocity.magnitude);
        if(Vector3.Distance(player.transform.position, transform.position) <= trackDist)
        {
            Quaternion angle = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, angle, Time.deltaTime * 5);
            nma.speed = speed;
            isTracking = true;
            nma.SetDestination(player.transform.position);
        }
        else
        {
            nma.speed = speed/2;
            isTracking = false;
        }
    }
    void Attack()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, player.transform.position)<= attackDist)
        {
            if (timer > cooldown)
            {
                timer = 0;
                ani.SetTrigger("trigAttack");
            }
        }
    }

    void Hit()
    {

    }

    void Dead()
    {

    }

    void DropProp()
    {

    }


    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        player = FindObjectOfType<PlayerControl>();

        origPos = transform.position;
        nma.stoppingDistance = attackDist;
    }
    void Start()
    {
        StartCoroutine(RandWalk());
    }
    
    void Update()
    {
        Move();
        Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == player.name)
        {
            other.GetComponent<PlayerControl>().Hit(attack, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.8f, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, attackDist);
        Gizmos.color = new Color(0, 0.8f, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, trackDist);
    }
}
