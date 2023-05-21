using UnityEngine;

/// <summary>
/// Handle input and move the player around
/// </summary>
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Bagpack bagpack;

    [SerializeField]
    private float rotationSpeed = 0.7f;
    [SerializeField]
    private float movementSpeed = 5f;
    private float gravity = -9.8f;
    private Vector2 previousPosition = Vector2.zero;

    [SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Locks the cursor
        Cursor.lockState = CursorLockMode.Confined;

        controller = GetComponent<CharacterController>();
        bagpack = GetComponent<Bagpack>();
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleDropping();
        HandleEquipItem();
        HandleFiring();
    }

    //Handle click to fire
    private void HandleFiring()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Has something equipped
            if(bagpack.equippedIndex >= 0)
            {
                if (bagpack.inventory[bagpack.equippedIndex].GetComponent<Weapon>().currentAmmo == 0)
                {
                    //try find ammo and apply
                    if (!bagpack.ApplyAmmo())
                    {
                        Debug.Log("Out of Ammo");
                        return;
                    }
                }

                GameObject bullet = Instantiate(bagpack.inventory[bagpack.equippedIndex].GetComponent<Weapon>().bulletPrefab);
                bullet.transform.forward = playerPrefab.transform.parent.GetChild(0).forward;
                bullet.transform.position = playerPrefab.transform.parent.GetChild(0).position;

                //If you need to reduce the ammo
                //bagpack.inventory[bagpack.equippedIndex].GetComponent<Weapon>().currentAmmo--;
            }
        }
    }

    //Handle movement
    private void HandleMovement()
    {
        //Store the inout in a Vector3
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), gravity, Input.GetAxis("Vertical"));

        //Rotation

        if (previousPosition == Vector2.zero)
            previousPosition = Input.mousePosition;

        Vector2 deltaPosition = (Vector2)Input.mousePosition - previousPosition;
        playerPrefab.transform.Rotate(new Vector3(-deltaPosition.y, deltaPosition.x)* rotationSpeed);
        previousPosition = Input.mousePosition;

        //Move using character controller
        controller.Move(input * Time.deltaTime * movementSpeed);
    }

    //Handle droppingequipped item
    private void HandleDropping()
    {
        if (Input.GetKeyDown(KeyCode.G))
            bagpack.DropItem();
    }

    //Handle Equipping item
    private void HandleEquipItem()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            bagpack.EquipItem(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            bagpack.EquipItem(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            bagpack.EquipItem(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            bagpack.EquipItem(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            bagpack.EquipItem(4);
    }
}
