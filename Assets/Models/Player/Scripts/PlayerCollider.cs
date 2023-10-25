 using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    [SerializeField] private TrailRenderer myTrail;
    void Start() {
        Debug.Log("HI");
    }

    void OnTriggerEnter(Collider other)
    {
        myTrail.Clear();
        myTrail.enabled = false;
    }


    void OnTriggerExit(Collider other)
    {
        myTrail.Clear();
        myTrail.enabled = true;
    }
}