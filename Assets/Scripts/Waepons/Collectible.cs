using UnityEngine;

[RequireComponent(typeof(Item))]
public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name);

        other.gameObject.GetComponent<Bagpack>().PickUpItem(GetComponent<Item>());
        Destroy(gameObject);
    }
}
