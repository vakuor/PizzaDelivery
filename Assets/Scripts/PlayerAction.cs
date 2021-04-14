using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] float enemySearchTime = 0.2f;
    float bulletSpeed = 10f;
    int enemyMask;
    bool isShooting = false;
    private Coroutine shootingRoutine;

    private AmmoData currentAmmoData;
    [HideInInspector] public AmmoData CurrentAmmoData { set { currentAmmoData = value; } }
    [SerializeField] private Transform projectile;
    [SerializeField] private AudioSource audioSource;

    private Vector3 rayCorrector = Vector3.up * 1f /*+ Vector3.forward*0.2f*/;
    public void SwitchShooting(){
        if(isShooting && shootingRoutine!=null){
            StopCoroutine(shootingRoutine);
        }
        else if(!isShooting){
            shootingRoutine = StartCoroutine(ShootCoroutine(currentAmmoData.GunFireRate));
        }
        isShooting = !isShooting;
    }
    private void Start() {
        enemyMask = LayerMask.NameToLayer("Enemy");
    }
    private void ShootTo(GameObject target){
        Transform proj = Instantiate(projectile, transform.position + rayCorrector, Quaternion.identity);
        Vector3 shootDirection = (target.transform.position-transform.position).normalized;
        proj.GetComponent<ProjectileMovement>().Setup(shootDirection, bulletSpeed, currentAmmoData.ShotPower);
        audioSource.clip = GameManager.instance.GetRandomAudioClip("Shoot");
        Debug.DrawLine(transform.position + rayCorrector, target.transform.position-transform.position, Color.green, 1f);
    }
    private EnemyBehaviour LookForEnemy(){
        //Debug.Log("LookForEnemy");
        List<EnemyBehaviour> enemies = GameManager.instance.Enemies;
        //Debug.Log(enemies.Count);
        if(enemies == null || enemies.Count<=0) return null;

        Ray ray;
        RaycastHit[] hit;

        EnemyBehaviour closestEnemy = null;
        float closestEnemyDistance = float.MaxValue;

        Vector3 position = transform.position + rayCorrector;
        Vector3 targetPosition;
        Vector3 targetDirection;

        foreach(EnemyBehaviour enemy in enemies){
            targetPosition = enemy.gameObject.transform.position + rayCorrector;
            //targetDirection = (position-targetPosition);
            targetDirection = (targetPosition-position).normalized;
            //Debug.DrawLine (position, targetPosition, Color.red, 2f);
            
            float distance = Vector3.Distance(position, targetPosition);
            if(closestEnemy!=null && distance >= closestEnemyDistance || distance > currentAmmoData.FireRange) continue;

            ray = new Ray(position, targetDirection);
            hit = Physics.RaycastAll(ray, distance+0.5f/*, LayerMask.NameToLayer("Player")*/);
            Debug.DrawLine(position, position+Vector3.up*2f, Color.green, 1f);
            Debug.DrawLine(targetPosition, targetPosition+Vector3.up*2f, Color.gray, 1f);
            //Debug.Log("TD: " + targetDirection);
            Debug.DrawRay(position, (targetPosition-position), Color.red, 2f);
            Debug.Log("Pass0");
            Transform parent = null;
            if (hit != null && hit.Length>0){
                Debug.Log(enemy.gameObject);
                parent = CustomUtils.FindMainParentByLayer(hit[0].transform, hit[0].transform.gameObject.layer);
                Debug.Log("Pass1");
            }
            if (hit != null && hit.Length>0 && parent != null && parent.gameObject == enemy.gameObject)
            {
                Debug.Log("Pass2");
                if(distance < closestEnemyDistance) {
                    Debug.Log("Pass3");
                    closestEnemy = enemy;
                    closestEnemyDistance = distance;
                }
                //if(hit[0].transform.parent.gameObject.layer == enemyMask && hit[0].transform.parent.gameObject == enemy.gameObject)
            }
        }
        if(closestEnemy!=null)
            Debug.DrawRay(position, (closestEnemy.transform.position+rayCorrector-position), Color.green, 2f);
        Debug.Log("Enemy now: " + closestEnemy);
        Debug.Log("Distance: " + closestEnemyDistance);
        return closestEnemy;
    }

    IEnumerator ShootCoroutine(float timeBeforeShooting)
    {
        EnemyBehaviour enemy = null;
        while(true)
        {
            if(enemy!=null && enemy.IsAlive){
                ShootTo(enemy.gameObject);
                yield return new WaitForSeconds(timeBeforeShooting);
                Debug.Log("ShootTime: "+ Time.time);
            }
            else {
                enemy = LookForEnemy();
                yield return new WaitForSeconds(enemySearchTime);
            }
        }
    }
}
