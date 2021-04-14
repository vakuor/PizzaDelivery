using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private GameEvent OnPlayerTowerAction;
    private Coroutine currentRoutine;
    
    private void Start() {
        OnPlayerTowerAction.RegisterListener(GetComponent<GameEventListener>());
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(other.name);
        OnPlayerTowerAction.Raise();
        //OnShootStart();
    }
    private void OnTriggerExit(Collider other) {
        //Debug.Log(other.name);
        OnPlayerTowerAction.Raise();
        //OnShootEnd();
    }
    public void OnShootStart(){
        currentRoutine = StartCoroutine(ShootCoroutine(1));
    }
    public void OnShootEnd(){
        StopCoroutine(currentRoutine);
    }

    IEnumerator ShootCoroutine(float timeBeforeShooting)
    {
        while(true)
        {
            yield return new WaitForSeconds(timeBeforeShooting);
            //Debug.Log("ShootTime: "+ Time.time);
        }
    }
}
