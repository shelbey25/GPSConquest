 using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    void OnTriggerEnter(Collider objectName)
    {
        Debug.Log("Entered collision with " + objectName.gameObject.name);
    }

    // Gets called during the stay of object inside the collider area
    void OnTriggerStay(Collider objectName)
    {
        Debug.Log("Colliding with " + objectName.gameObject.name);
    }

    // Gets called when the object exits the collider area
    void OnTriggerExit(Collider objectName)
    {
        Debug.Log("Exited collision with " + objectName.gameObject.name);
    }
}