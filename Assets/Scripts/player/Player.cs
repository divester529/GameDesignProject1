using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private GameManager gameManager;

  // Health
  public int health;
  public int maxHealth;

  public int strength;

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

  }

  // Update is called once per frame
  void Update()
  {

  }
}
