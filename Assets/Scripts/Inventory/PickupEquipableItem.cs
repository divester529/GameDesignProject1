using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEquipableItem : MonoBehaviour {

  [SerializeField] Inventory inventory;
  [SerializeField] EquipableItem Weapon;

  void OnTriggerEnter2D(Collider2D other) {
    if (other.name == "Player") {
      inventory.AddItem(Weapon);
      Destroy(gameObject);
    }
  }

  public void Start() {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
  }
}
