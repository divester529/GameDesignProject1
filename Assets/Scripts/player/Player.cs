using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
private GameManager gameManager;

    // Health
    public int health;
    public int maxHealth;

    public Inventory inventory;

    public int strength;

    // Combat stats
    public int damage;
    public float swingTime=1; // Time (in seconds) between each attack
    public float knockback = 0.75f;

    public bool isAttacking;

    public float cooldown; // Swing cooldown

    public Vector2 colSize; // This is for some hacky ass code to make knockback

    // Game over screen
    [SerializeField] GameObject gameOver;

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

    void onDeath()
    {
        gameOver.SetActive(true);
    }

    public void reset()
    {
        health = maxHealth = 100;
        damage = 10;
        swingTime = 1; // Time (in seconds) between each attack
        knockback = 0.75f;
        inventory.ResetUI();
    }

    void Start()
    {
        gameManager=GameManager.Instance;
        colSize = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y);
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
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
        if (health <= 0) {
          onDeath();
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
