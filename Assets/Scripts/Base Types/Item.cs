using UnityEngine;

[System.Serializable]
public class Item: MonoBehaviour
{
    public GameObject prefab;
    public int requiredSpace;
    public string itemName;
    public bool canEquip = false;
}
