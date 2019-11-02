using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
private GameManager gameManager;

    // Health
    public int health;
    public int maxHealth;

    // Combat stats
    public int damage=10;
    public float swingTime=1; // Time (in seconds) between each attack
    public float knockback = 0.75f;

    public bool isAttacking;
    
    public float cooldown; // Swing cooldown

    public Vector2 colSize; // This is for some hacky ass code to make knockback

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    protected Player()
    {
        health=maxHealth=100;
    }

    void Start()
    {
        gameManager=GameManager.Instance;
        colSize = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y);
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isAttacking = false;
        }
        
    }

    void FixedUpdate()
    {
        if (isAttacking && cooldown<=0)
        {
            Debug.Log("Swinging");
            gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colSize.x + knockback, colSize.y + knockback);
            //isAttacking = false;
            Debug.Log(gameObject.GetComponent<BoxCollider2D>().size);
            cooldown = swingTime;
        }
        else
        {
            if (colSize.x != gameObject.GetComponent<BoxCollider2D>().size.x)
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(colSize.x, colSize.y);
                Debug.Log(gameObject.GetComponent<BoxCollider2D>().size);
            }
            if (cooldown >= 0)
                cooldown -= Time.fixedDeltaTime;
        }
    }

}
