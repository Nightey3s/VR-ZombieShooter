using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    // walking speed
    public float speed = 0.5f;

    // angular speed
    public float angularSpeed = 1;

    // distance at which the zombie will chase the player
    public float chasingDistance = 20;

    // rigid body component
    Rigidbody rb;

    // animator
    Animator anim;

    // player
    PlayerContoller player;

    // available states
    enum State { idle, attacking, dead };
    
    // current state
    State currentState = State.idle;

    void Awake()
    {
        //get component
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        // get player
        player = FindObjectOfType<PlayerContoller>();

        if (player == null) Debug.LogError("There needs to be a PlayerController in the scene");

        // search for the player at an interval of 0.5 seconds
        InvokeRepeating("LookForPlayer", 0, 0.5f);
    }

    void LookForPlayer()
    {
        // only look if the zombie is idle
        if (currentState != State.idle) return;
        
        // Check distance
        if(Vector3.Distance(player.transform.position, transform.position) <= chasingDistance)
        {
            // change state to attacking
            currentState = State.attacking;

            // activate attack animation
            anim.SetBool("sawPlayer", true);

            // cancel the looking for player invokation
            CancelInvoke();
        }
    }

    void FixedUpdate()
    {
        // move the parent to the position of the child (the model)
        transform.position = transform.GetChild(0).position;

        // set the child to be in the origin of the parent
        transform.GetChild(0).localPosition = Vector3.zero;

        // only chase if we are attacking!
        if (currentState != State.attacking) return;       

        // instant rotation of the transform: 
        transform.LookAt(player.transform.position);    
    }

    void OnTriggerEnter(Collider other)
    {
        // check if a bullet has hit us
        if(other.CompareTag("Bullet"))
        {
            // change the state to dead
            currentState = State.dead;

            // activate death animation
            anim.SetBool("isAlive", false);

            // disable collider
            GetComponent<Collider>().enabled = false;
            rb.isKinematic = true;
        }
    }
}
