using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 direction;
    private float moveSpeed;
    private float shotPower;
    [SerializeField] private new Rigidbody rigidbody;
    public void Setup(Vector3 direction, float moveSpeed, float shotPower){
        this.direction = direction;
        this.moveSpeed = moveSpeed;
        this.shotPower = shotPower;
        rigidbody.velocity = direction*moveSpeed;
        Destroy(gameObject, 8f);
    }
    private void FixedUpdate() {
        //transform.position += direction*moveSpeed*Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) //Debug.Log("Yes");
        //else Debug.Log("No");
        {
            Transform parent = CustomUtils.FindMainParentByLayer(other.gameObject.transform, LayerMask.NameToLayer("Enemy"));
            
            parent.GetComponent<EnemyBehaviour>().OnDeath();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            Debug.Log("Kin: "+rb.isKinematic);
            rb.AddForce(direction*shotPower/*direction*moveSpeed*100*/);
            Debug.Log("Hit: " + parent.name);
            Destroy(gameObject);
        }

    }
}
