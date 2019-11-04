using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] EquipableItem currentWeapon;
    [SerializeField] int currentPotionIndex;
    [SerializeField] ConsumableItem currentPotion;
    [SerializeField] List<Item> items;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;
    [SerializeField] GameObject player;
    [SerializeField] EquipableItem startingItem;
    [SerializeField] int maxItemsSize;

    [SerializeField] List<GameObject> SpawnableItems;

    void Start() {
      int i = 0;
      for(; i < maxItemsSize; i++) {
        items.Add(null);
      }
      AddItem(startingItem);
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

    public void AddItem(EquipableItem item) {
        if (items[0] != null)
        {
            RemoveItem(currentWeapon);
            SpawnItem(currentWeapon.index);
        }
      currentWeapon = item;
      player.GetComponent<Player>().strength += item.StrengthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed += item.AgilityBonus;
      items.RemoveAt(0);
      items.Insert(0, item);
      RefreshUI();
      return;
    }

    public void RemoveItem(EquipableItem item) {
      player.GetComponent<Player>().strength -= item.StrengthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed -= item.AgilityBonus;
      items.RemoveAt(0);
      items.Insert(0, null);
      RefreshUI();
      return;
    }

    public void AddItem(ConsumableItem item) {
      if (items[1] != null) {
        RemoveItem(currentPotion);
        SpawnItem(currentPotionIndex);
      }
      currentPotionIndex = item.index;
      currentPotion = item;
      items.RemoveAt(1);
      items.Insert(1, item);
      RefreshUI();
      return;
    }

    public void RemoveItem(ConsumableItem item) {
      items.RemoveAt(1);
      items.Insert(1, null);
      currentPotion = null;
      RefreshUI();
      return;
    }

    public void SpawnItem(int index) {
      Vector3 spawnPosition = new Vector3(player.transform.position.x + 1.5f, player.transform.position.y, player.transform.position.z);
      var temp = GameObject.Instantiate(SpawnableItems[index], spawnPosition, player.transform.rotation);
      return;
    }

    public void ResetUI() {
      items.RemoveAt(0);
      items.Insert(0, startingItem);
      items.RemoveAt(1);
      items.Insert(1, null);
      RefreshUI();
    }

    void UsePotion() {
      player.GetComponent<Player>().health += currentPotion.HealthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed *= currentPotion.MovementBonus;
      RemoveItem(currentPotion);
    }

    void Update() {
      if (Input.GetKeyDown("2")) {
        UsePotion();
      }
    }
}
