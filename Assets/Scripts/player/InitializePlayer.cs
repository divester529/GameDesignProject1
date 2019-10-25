using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
  private GameManager gameManager;
  // Start is called before the first frame update
  void Start()
  {
      gameManager=GameManager.Instance;

      // Store the player in the game manager
      gameManager.setPlayer(gameObject);

      Debug.Log(gameManager.getPlayer().name);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
