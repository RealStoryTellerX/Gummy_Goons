using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SwordController : MonoBehaviour
{
    public GameObject player;
    //private StarterAssetsInputs _input;
    private PlayerInput _playerInput;
    public bool isAttacking = false;
    public ThirdPersonController progress = null;
    //private PlayerInputActions playerInputActions;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Awake()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0.0469999984f, 1.16299999f, 0.312000006f);
        
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Touching Enemy");
        if (isAttacking)
        {
            //Debug.Log("Touching Enemy");
            if (other.gameObject.name == "Enemy") { 
                Debug.Log("Touching Enemy");
                Destroy(other.gameObject);
                progress = GameObject.Find("PlayerArmature").GetComponent<ThirdPersonController>();
                progress.hasKilled = true;
            }
        }
        isAttacking = false;
    }

    
}
