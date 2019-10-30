using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
  Weapon
}

[CreateAssetMenu]
public class EquipableItem : Item
{
  public int StrengthBonus;
  public int AgilityBonus;
  [Space]
  public float StrengthPercentBonus;
  public float AgilityPercentBonus;
  [Space]
  public EquipmentType EquipmentType;
}
