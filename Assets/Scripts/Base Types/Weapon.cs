using UnityEngine;

public class Weapon : Item
{
    public int ammoCapacity;
    public int currentAmmo;

    public GameObject requiredAmmo;
    public GameObject bulletPrefab;

    public Transform bulletFirePoint;
}
