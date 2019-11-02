using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    GameManager gm;
    [SerializeField] EquipableItem item;
    [SerializeField] GameObject player;

    void Start() {
      //gm = GameManager.Instance;
      AddItem(item);
      RemoveItem(item);
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
      Debug.Log("Help me");
      if (IsFull()) {
        return false;
      }
      player.GetComponent<Player>().strength += item.StrengthBonus;
      items.Add(item);
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
