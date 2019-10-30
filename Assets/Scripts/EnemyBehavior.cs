﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // GameManager singleton
    private GameManager gameManager;

    // These fields will be different for the different classes of enemies
    // Change them in the inspector
    public float maxHealth=100;
    public float currHealth=100;

    public int damage=10;

    public float attackSpeed=5000;
    public float movementSpeed=1;
    // This will be the maximum distance the enemy will chase the player from.
    public float chaseDistance=100;

    private Transform transform;
    private Rigidbody2D rb;

    private bool touchingPlayer=false; 
    private float attackTimer=0; // Time until next attack

    void Start()
    {
        gameManager = GameManager.Instance;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        Debug.Log(Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check to see if the unit made contact with the player
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.transform.name);

        // If the other object is the player, we are now touching the player
        if(string.Equals(other.transform.name, "Player"))
        {
            Debug.Log("We are touching the batman");
            touchingPlayer = true;
        }
    }

    // Check to see if the unit exited contact with the player
    void OnCollisionExit2D(Collision2D other)
    {
        // If the other object is the player, we are no longer touching the player
        if(string.Equals(other.transform.name, "Player"))
        {
            touchingPlayer = false;
        }
    }
    
    // If the enemy is touching the player do damage to player every time attackTimer reaches 0
    void attackPlayer()
    {
        if(touchingPlayer)
        {
            // If we're ready to attack, "attack" the player (subtract hitpoints = to damage)
            if (attackTimer == 0)
            {
                gameManager.getPlayer().GetComponent<Player>().health -= damage;
                attackTimer = attackSpeed;
            }
            // Otherwise increment timer
            else
            {
                attackTimer -= Time.fixedDeltaTime;
            }
          
        }
    }

    void FixedUpdate()
    {
        // Get the vector difference between the enemy position and the player position
        Vector3 playerPos = gameManager.getPlayer().GetComponent<Transform>().position;
        Vector3 distToPlayer = transform.position - playerPos;
        Vector2 movement;

        float dx=0, dy=0;

        attackPlayer();

        // If the magnitude of the distance is within the enemies chase radius, move towards the player
        if(distToPlayer.magnitude<=chaseDistance)
        {
            if(playerPos.x>transform.position.x)
            {
                dx = 1;
            }
            if (playerPos.x < transform.position.x)
            {
                dx = -1;
            }
            if (playerPos.y > transform.position.y)
            {
                dy = 1;
            }
            if (playerPos.y < transform.position.y)
            {
                dy = -1;
            }

            movement = new Vector2(dx, dy);
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
    }
}