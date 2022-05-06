using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel : MonoBehaviour
{ 
    [SerializeField] private Collider _carCollider;
    private void Start()
    {
        if(CollisionManager._instance != null)
        CollisionManager._instance._playerCollider = _carCollider;
    }
    
    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        CollisionManager._instance.HandleCollision(col.collider.gameObject);
    }
    private void OnTriggerEnter(Collider col)
    {
        CollisionManager._instance.HandleCollision(col.gameObject);
    }
    private void OnTriggerExit(Collider col)
    {
        CollisionManager._instance.HandleCollision(col.gameObject);
    }
}
