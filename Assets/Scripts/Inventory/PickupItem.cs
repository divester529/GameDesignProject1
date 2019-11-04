using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class PickupItem : MonoBehaviour {

  [SerializeField] Inventory inventory;
  [SerializeField] Item Weapon;

  void OnTriggerEnter2D(Collider2D other) {
    inventory.AddItem(Weapon);
    Destroy(gameObject);
  }

  public void Start() {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
=======
public class PickupItem : Inventory
{
  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {

    }
>>>>>>> b574683651de11d3b0df8ffa3b349009f08cc643
  }
}
