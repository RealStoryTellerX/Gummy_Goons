using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;  // Detection range for player
    [SerializeField] private LayerMask layerMask; // Layer mask to exclude certain objects from raycast detection

    private Transform target;   // Reference to the player
    private NavMeshAgent agent; // Reference to the NavMeshAgent
    private CharacterCombat combat;
    private float attackDelay = 1f; // Delay between attacks
    private float lastAttackTime = 0f; // Time of last attack

    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("PlayerArmature").transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            // Move towards the target
            agent.SetDestination(target.position);

            // If within attacking distance
            if (distance <= agent.stoppingDistance)
            {
                // Check if enough time has passed since the last attack
                if (Time.time - lastAttackTime >= attackDelay)
                {
                    // Check if there are any obstacles between the enemy and the target
                    RaycastHit hit;
                    if (!Physics.Raycast(transform.position, (target.position - transform.position).normalized, out hit, Mathf.Infinity, layerMask))
                    {
                        // Attack the target and reset the last attack time
                        CharacterStats targetStats = target.GetComponent<CharacterStats>();
                        if (targetStats != null)
                        {
                            combat.Attack(targetStats);
                            lastAttackTime = Time.time;
                        }
                    }
                }

                FaceTarget();   // Make sure to face towards the target
            }
        }
    }

    // Rotate to face the target
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
