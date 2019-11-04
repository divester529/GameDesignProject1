using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Prototype for Random Generator
public class RandomItemGenerator : MonoBehaviour {
  public GameObject[] items;
  public Transform spawnPos;

  int rand;

  void Update () {
    if (Input.GetMouseButtonDown(0)) {
      SpawnRandom();
    }
  }

  void SpawnRandom() {
    rand = Random.Range(0, items.Length);
    Instantiate(items[rand], spawnPos.position, spawnPos.rotation);
  }
}
