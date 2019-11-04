using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Scripts/Inventory/PickupEquipableItem.cs
public class PickupEquipableItem : MonoBehaviour {
=======
<<<<<<< HEAD
public class PickupItem : MonoBehaviour {
>>>>>>> enemy:Assets/Scripts/Inventory/PickupItem.cs

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
=======
public class PickupItem : Inventory
{
  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {

    }
>>>>>>> b574683651de11d3b0df8ffa3b349009f08cc643
  }
}
