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

//At start, create two null fields to get size up to number of fields
    void Start() {
      int i = 0;
      for(; i < maxItemsSize; i++) {
        items.Add(null);
      }
      AddItem(startingItem);
    }

//Allows the images to be transparent if null
    private void OnValidate() {
      if (itemsParent != null) {
        itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();
      }
      RefreshUI();
    }

//Refreshes UI
    private void RefreshUI() {
      int i = 0;
      for(; i < items.Count && i < itemSlots.Length; i++) {
        itemSlots[i].Item = items[i];
      }
      for (; i < itemSlots.Length; i++) {
        itemSlots[i].Item = null;
      }
    }

//Method to add Weapons
    public void AddItem(EquipableItem item) {
      if (items[0] != null) {
        RemoveItem(currentWeapon);
        SpawnItem(currentWeapon.index);
      }
      currentWeapon = item;
      player.GetComponent<Player>().damage += item.StrengthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed += item.AgilityBonus;
      items.RemoveAt(0);
      items.Insert(0, item);
      RefreshUI();
      return;
    }

//Method to Remove Weapon
    public void RemoveItem(EquipableItem item) {
      player.GetComponent<Player>().damage -= item.StrengthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed -= item.AgilityBonus;
      items.RemoveAt(0);
      items.Insert(0, null);
      RefreshUI();
      return;
    }

//Method to add Consumable Item
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

//Method to Remove UI
    public void RemoveItem(ConsumableItem item) {
      items.RemoveAt(1);
      items.Insert(1, null);
      currentPotion = null;
      RefreshUI();
      return;
    }

//Spawns item from the list of items to the right of the Player
    public void SpawnItem(int index) {
      Vector3 spawnPosition = new Vector3(player.transform.position.x + 1.5f, player.transform.position.y, player.transform.position.z);
      var temp = GameObject.Instantiate(SpawnableItems[index], spawnPosition, player.transform.rotation);
      return;
    }

//After losing game, UI is reset
    public void ResetUI() {
      items.RemoveAt(0);
      items.Insert(0, startingItem);
      items.RemoveAt(1);
      items.Insert(1, null);
      RefreshUI();
    }

//Potion Functionallity
    void UsePotion() {
      player.GetComponent<Player>().health += currentPotion.HealthBonus;
      player.GetComponent<PlayerMovement>().movementSpeed *= currentPotion.MovementBonus;
      RemoveItem(currentPotion);
    }

//Allows Keybinds: Using potion
    void Update() {
      if (Input.GetKeyDown("2")) {
        UsePotion();
      }
    }

//After killing enemy, generate random number and spawn random item
    public void RandomSpawn() {
      Debug.Log("Nice");
      int rand = Random.Range(0, SpawnableItems.Count);
      SpawnItem(rand);
    }
}
