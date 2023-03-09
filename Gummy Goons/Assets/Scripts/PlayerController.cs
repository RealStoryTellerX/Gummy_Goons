using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initialSpeed = 10f;
    public float reducedSpeed = 5f;
    public float enemyRange = 5f;
    public float speedReductionInterval = 15f;

    private float currentSpeed;
    private float timeSinceLastReduction = 0f;

    private void Start()
    {
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
        // Check if any enemies are within range
        bool enemyInRange = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= enemyRange)
            {
                enemyInRange = true;
                break;
            }
        }

        // If an enemy is in range, reduce the player's speed gradually over time
        if (enemyInRange)
        {
            timeSinceLastReduction += Time.deltaTime;
            if (timeSinceLastReduction >= speedReductionInterval)
            {
                currentSpeed = Mathf.Max(currentSpeed - reducedSpeed, 0f);
                timeSinceLastReduction = 0f;
            }
        }
        // If no enemy is in range, reset the player's speed
        else
        {
            currentSpeed = initialSpeed;
            timeSinceLastReduction = 0f;
        }

        // Move the player
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}

