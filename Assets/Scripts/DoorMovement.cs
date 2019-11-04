using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : MonoBehaviour
{
    private GameManager gameManager;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        playerTransform = gameManager.getPlayer().GetComponent<Transform>();

        //if the player goes through the top door, increase their y
        if (this.tag == "doorTopOpen")
            if ((int)playerTransform.position.x == (int)this.transform.position.x && (int)playerTransform.position.y == (int)this.transform.position.y)
            {
                Debug.Log("Up");
                playerTransform.position = new Vector3(playerTransform.position.x, (int)playerTransform.position.y + 3.0f, playerTransform.position.z);
            }
        //if the player goes through the left door, decrease their x
        if (this.tag == "doorLeftOpen")
            if ((int)playerTransform.position.x == (int)this.transform.position.x && (int)playerTransform.position.y == (int)this.transform.position.y)
            {
                Debug.Log("Left");
                playerTransform.position = new Vector3(playerTransform.position.x - 3.0f, playerTransform.position.y, playerTransform.position.z);
            }
        //if the player goes through the right door, increase their x
        if (this.tag == "doorRightOpen")
            if ((int)playerTransform.position.x == (int)this.transform.position.x-1 && (int)playerTransform.position.y == (int)this.transform.position.y)
            {
                Debug.Log("Right");
                playerTransform.position = new Vector3(playerTransform.position.x + 3.0f, playerTransform.position.y, playerTransform.position.z);
            }
        //if the player goes through the bottom door, decrease their y
        if (this.tag == "doorBottomOpen")
            if ((int)playerTransform.position.x == (int)this.transform.position.x && (int)playerTransform.position.y-1 == (int)this.transform.position.y)
            {
                Debug.Log("Down");
                playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 3.0f, playerTransform.position.z);
            }
    }
}