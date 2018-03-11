using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : StatsManager
{
    private void Start()
    {
        EquipmentManager.instance.onEqupmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.AddModifier(oldItem.damageModifier);
        }
    }
}
