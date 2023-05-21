using UnityEngine;

/// <summary>
/// Rotates the gameobject at a constant speed
/// </summary>
public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 0.01f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
