using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Not needed, camera tracks parent object */
public class cameraController : MonoBehaviour
{
  private GameManager gameManager;

  private Transform camTransform;
  private Transform playerTransform;
  private float lastWidth;
  private float lastHeight;

  // Start is called before the first frame update
  void Start()
  {
        // Get the gameManager Singleton
        gameManager = GameManager.Instance;

        // Get the camera transform matrix
        camTransform = GetComponent<Transform>();



        //Camera.aspect(1);
  }

  // Update is called once per frame
  void Update()
  {

        //find the room the player is in and center it there (round the coordinates to multiples of the room size)
        playerTransform=gameManager.getPlayer().GetComponent<Transform>();

        if((int)playerTransform.position.x / gameManager.roomSizeX != ((int)camTransform.position.x) / gameManager.roomSizeX || (int)playerTransform.position.y / gameManager.roomSizeY != (int)camTransform.position.y / gameManager.roomSizeY){
            camTransform.position = new Vector3(((int)playerTransform.position.x / gameManager.roomSizeX) * gameManager.roomSizeX + (0.5f * gameManager.roomSizeX-0.5f), ((int)playerTransform.position.y / gameManager.roomSizeY) * gameManager.roomSizeY + (0.5f * gameManager.roomSizeY+0.5f), camTransform.position.z);

        }

    }

}
