using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
  private GameManager gameManager;

  private Transform camTransform;
  private Transform playerTransform;

  // Start is called before the first frame update
  void Start()
  {
    // Get the gameManager Singleton
    gameManager = GameManager.Instance;

    // Get the camera transform matrix
    camTransform = GetComponent<Transform>();
  }

  // Update is called once per frame
  void Update()
  {
    playerTransform=gameManager.getPlayer().GetComponent<Transform>();

    if(playerTransform.position.x!=camTransform.position.x || playerTransform.position.y!=camTransform.position.y){
      camTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, camTransform.position.z);
    }
  }

}
