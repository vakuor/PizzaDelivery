using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private AmmoData currentAmmoData;
    [HideInInspector] public AmmoData CurrentAmmoData { set { currentAmmoData = value; } }

    public LayerMask navMeshMask;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = currentAmmoData.GunnerSpeed;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.instance.IsGameRunning){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Debug.Log(Input.mousePosition);
            if(Physics.Raycast(ray, out hitInfo, 1000, navMeshMask)){
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
