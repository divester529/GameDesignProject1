using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======
public enum EquipmentType {
  Weapon
}

>>>>>>> EnemiesSpawnInRooms
[CreateAssetMenu]
public class EquipableItem : Item
{
  public int StrengthBonus;
<<<<<<< HEAD
  public float AgilityBonus;
  public int index;
=======
  public int AgilityBonus;
  [Space]
  public float StrengthPercentBonus;
  public float AgilityPercentBonus;
  [Space]
  public EquipmentType EquipmentType;
>>>>>>> EnemiesSpawnInRooms
}
