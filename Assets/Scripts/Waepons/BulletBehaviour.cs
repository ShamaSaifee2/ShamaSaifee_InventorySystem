using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteAfterDelay", 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * bulletSpeed;
    }

    private void DeleteAfterDelay()
    {
        Destroy(gameObject);
    }
}
