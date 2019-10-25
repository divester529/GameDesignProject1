using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script will show/hide healthTicks based on player health
public class HealthTick : MonoBehaviour
{
  private GameManager gameManager;
  private RectTransform transform;

  public int threshold; // The threshold for this tick (Set in inspecter)

  // Start is called before the first frame update
  void Start()
  {
    gameManager=GameManager.Instance;

    transform = GetComponent<RectTransform>();
  }

  // Update is called once per frame
  void Update()
  {
    if(gameManager.getPlayer().GetComponent<Player>().getHealth()<threshold)
    {
      transform.localScale = new Vector3(0, 0, 0);
    }
    else{
      transform.localScale = new Vector3(1, 1, 1);
    }
  }
}
