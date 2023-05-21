using System.Collections.Generic;
using UnityEngine;

public class Bagpack : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] private int bagCapacity;
    [SerializeField] private Transform equipParent;

    public int equippedIndex = -1;

    //Equip selected Item
    public void EquipItem(int index)
    {
        if (index < inventory.Count && index >= 0)
        {
            Item item = inventory[index].GetComponent<Item>();

            if (item.canEquip)
            {
                Debug.Log("Can equip " + item.itemName);
                //If there is an item equipped, destroy the object
                if (equipParent.childCount > 0)
                    Destroy(equipParent.GetChild(0).gameObject);

                //Spawn the equipped item in place
                GameObject obj = Instantiate(inventory[index], equipParent);
                obj.name = inventory[index].name;

                equippedIndex = index;
            }
        }            
    }

    //Pickup an item
    public void PickUpItem(Item item)
    {
        if(inventory.Count == bagCapacity)
        {
            Debug.Log("Inventory is full!");
            return;
        }

        inventory.Add(item.prefab);
    }

    //Drop an item from backpack
    public void DropItem()
    {
        if (equippedIndex < inventory.Count && equippedIndex >= 0)
        {
            //Delete the equipped object
            if (equipParent.childCount > 0)
                Destroy(equipParent.GetChild(0).gameObject);

            inventory.Remove(inventory[equippedIndex]);

            //Equip the next equippable item
            for(int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].GetComponent<Item>().canEquip)
                {
                    EquipItem(i);
                    return;
                }

                equippedIndex = -1;
            }
        }
    }

    //Try to find ammo and add to the equipped weapon
    public bool ApplyAmmo()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            //Found a required ammo in the inventory
            if(inventory[i].GetComponent<Weapon>())
            {
                if (inventory[i].GetComponent<Weapon>().requiredAmmo == inventory[equippedIndex].GetComponent<Item>().prefab)
                {
                    inventory[i].GetComponent<Weapon>().currentAmmo = inventory[equippedIndex].GetComponent<Ammo>().ammunationCount;
                    RemoveItem(i);
                    return true;
                }
            }
        }
        return false;
    }

    private void RemoveItem(int index)
    {
        if (equippedIndex < inventory.Count && equippedIndex >= 0)
        {
            if (index == equippedIndex)
            {
                equippedIndex = -1;
                //Delete the equipped object
                if (equipParent.childCount > 0)
                    Destroy(equipParent.GetChild(0).gameObject);

                //Equip the next equippable item
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].GetComponent<Item>().canEquip && i != equippedIndex)
                    {
                        EquipItem(i);
                        return;
                    }
                }
            }

            inventory.Remove(inventory[index]);
        }
    }
}
