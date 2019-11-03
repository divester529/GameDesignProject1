using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] GameObject player;
    [SerializeField] int maxItemsSize;

    void Start() {
      int i = 0;
      for(; i < maxItemsSize; i++) {
        items.Add(null);
      }
    }

    private void OnValidate() {
      if (itemsParent != null) {
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
      }
      RefreshUI();
    }

    private void RefreshUI() {
      int i = 0;
      for(; i < items.Count && i < itemSlots.Length; i++) {
        itemSlots[i].Item = items[i];
      }
      for (; i < itemSlots.Length; i++) {
        itemSlots[i].Item = null;
      }
    }

    public bool AddItem(EquipableItem item) {
      if (items[0] != null) {
        return false;
      }
      player.GetComponent<Player>().strength += item.StrengthBonus;
      items.RemoveAt(0);
      items.Insert(0, item);
      RefreshUI();
      return true;
    }

    public bool AddItem(ConsumableItem item) {
      if (items[1] != null) {
        return false;
      }
      player.GetComponent<Player>().health += item.HealthBonus;
      items.RemoveAt(1);
      items.Insert(1, item);
      RefreshUI();
      return true;
    }

    public bool RemoveItem(EquipableItem item) {
      if (items.Remove(item)) {
        player.GetComponent<Player>().strength -= item.StrengthBonus;
        items.Remove(item);
        RefreshUI();
        return true;
      }
      return false;
    }

    public bool IsFull() {
      return items.Count >= itemSlots.Length;
    }
}
