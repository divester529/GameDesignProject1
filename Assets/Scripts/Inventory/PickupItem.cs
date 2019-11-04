using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Inventory
{
  void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")) {

    }
  }
}
