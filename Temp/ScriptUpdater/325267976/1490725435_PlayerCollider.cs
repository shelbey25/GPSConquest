 using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    [SerializeField] private TrailRenderer myTrail;
    void Start() {
        Debug.Log("HI");

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.material.color);
        if ((other.GetType() == typeof(MeshCollider))) {
            MeshCollider altCol = (MeshCollider)other;
            Debug.Log("MESH");
            MeshRenderer meshRenderer = GetComponent<Collider>().gameObject.GetComponent<MeshRenderer>();
            Debug.Log(meshRenderer.material.color);
        }
        myTrail.Clear();
        myTrail.enabled = false;
    }


    void OnTriggerExit(Collider other)
    {
        myTrail.Clear();
        myTrail.enabled = true;
    }
}