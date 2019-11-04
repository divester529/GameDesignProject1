using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to interact with Consumable Items on Ground
public class PickupConsumableItem : MonoBehaviour {

  [SerializeField] Inventory inventory;
  [SerializeField] ConsumableItem Potion;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.name == "Player") {
      inventory.AddItem(Potion);
      Destroy(gameObject);
    }
  }

  public void Start() {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
  }
}
