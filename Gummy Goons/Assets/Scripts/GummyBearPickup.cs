using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GummyBearPickup : MonoBehaviour
{
    // Reference to the player object
    public GameObject player;

    // Reference to the gummy bear object
    public GameObject gummyBear;

    // Flag to check if the gummy bear has been picked up
    private bool pickedUp = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is close enough to the gummy bear
        float distance = Vector3.Distance(player.transform.position, gummyBear.transform.position);
        if (distance < 1.0f)
        {
            // Check if the player presses the pickup button
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // Destroy the gummy bear and set the pickedUp flag
                Destroy(gummyBear);
                pickedUp = true;
            }
        }
    }

    // Draw a gizmo to show the pickup radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    // Check if the gummy bear has been picked up
    public bool IsPickedUp()
    {
        return pickedUp;
    }
}
