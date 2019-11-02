using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

  [SerializeField] Inventory inventory;
  [SerializeField] EquipableItem Weapon;

  void OnTriggerEnter2D(Collider2D other) {
    inventory.AddItem(Weapon);
    Destroy(gameObject);
  }

  public void Start() {
    inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
  }
}
