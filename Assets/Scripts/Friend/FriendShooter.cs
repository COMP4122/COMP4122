using System.Collections;
using UnityEngine;

public class FriendShooter : MonoBehaviour {

    public BasicBullet bullet;


    public int minBullets = 1;
    public int maxBullets = 3;

    public float minShootInterval = 4f;
    public float maxShootInterval = 8f;

    private WaitForSeconds intervalBetweenBullets = new WaitForSeconds(.1f);

    private Player player;
    public GameObject friendExplosion;

    public Transform flagTransform;
    public float moveSpeed;

    // AI
    public GameObject shootTarget;
    private enum EnemyState {walking, shooting};
    private EnemyState currentState;

    private float timeLastShoot;
    public float shootRange;
    public float shootCD = 1f;

    void Start() {
        player = GetterUtility.GetPlayer();
        flagTransform = GetterUtility.GetEnemyFlag().transform;
        currentState = EnemyState.walking;
    }

    void Update() {
        switch (currentState) {
            case EnemyState.walking:
                WalkTo(flagTransform);
                Debug.Log("In walking state");
                break;
            case EnemyState.shooting:
                Debug.Log("In shooting state");
                Shoot();
                break;
            default:
                Debug.Log("Something's wrong with EnemyState");
                break;
        }
    }

    void SwitchToWalkingState() {
        currentState = EnemyState.walking;
    }

    void SwitchToShootingState() {
        currentState = EnemyState.shooting;

    }

    void UpdateShootTarget(GameObject newTarget) {
        shootTarget = newTarget;
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy" && currentState != EnemyState.shooting) {
            Debug.Log("Target Found: " + collider.gameObject.name);
            SwitchToShootingState();
            UpdateShootTarget(collider.gameObject);
        }
    }



    void WalkTo(Transform walkTarget) {
        Vector3 moveDirection = walkTarget.position - this.transform.position;
        moveDirection = moveDirection.normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        Debug.Log(moveDirection);
    }

    void Shoot() {
        if (shootTarget == null) {
            SwitchToWalkingState();
        } else if (IsShootReady() && IsShootTargetInRange()) {
            ShootAtTarget(shootTarget);
        } else if (!IsShootTargetInRange()){
            WalkTo(shootTarget.transform);
            Debug.Log("Walking to: " + shootTarget.transform.position);
        } else {
            Debug.Log("Hey I'm standing here doing nothing");
        }
    }

    bool IsShootTargetInRange() {
        float distance = (shootTarget.transform.position - this.transform.position).magnitude;
        return distance < shootRange;
    }

    bool IsShootReady() {
        return Time.time - timeLastShoot >= shootCD;
    }

    /*IEnumerator ShootPlayer() {
        while (true) {
            WaitForSeconds wfsShootInterval = new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            yield return wfsShootInterval;
            for (int i = 0; i < Random.Range(minBullets, maxBullets); i++) {
                EnemyBullet newEnemyBullet = Instantiate(bullet).GetComponent<EnemyBullet>();
                newEnemyBullet.ShootWithRay(GetShootRay(player.transform.position));
                yield return intervalBetweenBullets;
            }
        }
    }*/

    void ShootAtTarget(GameObject target) {
        BasicBullet newBasicBullet = Instantiate(bullet).GetComponent<BasicBullet>();
        Ray ray = new Ray(this.transform.position, target.transform.position - this.transform.position);
        newBasicBullet.ShootWithRay(ray);
        timeLastShoot = Time.time;
    }


    IEnumerator ShootAtTargetCoroutine(GameObject target) {
        while (true) {
            WaitForSeconds wfsShootInterval = new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            yield return wfsShootInterval;
            for (int i = 0; i < Random.Range(minBullets, maxBullets); i++) {
                EnemyBullet newEnemyBullet = Instantiate(bullet).GetComponent<EnemyBullet>();
                Ray ray = new Ray(this.transform.position, target.transform.position - this.transform.position);
                newEnemyBullet.ShootWithRay(ray);
                yield return intervalBetweenBullets;
            }
        }
    }

    private Ray GetShootRay(Vector3 targetPosition) {
        Vector3 shootDirection = targetPosition - transform.position;
        Ray shootRay = new Ray(transform.position + 1f * shootDirection.normalized, shootDirection);
        return shootRay;
    }

    public void Die() {
        GameObject obj = (GameObject) Instantiate(friendExplosion, transform.position, transform.rotation);
        Destroy(obj, 3);
        Destroy(this.gameObject);
    }
}
