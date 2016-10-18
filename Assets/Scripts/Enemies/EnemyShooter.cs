using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public EnemyBullet bullet;

    public int minBullets = 1;
    public int maxBullets = 3;

    public GameObject enemyExplosion;

    private float minShootInterval = 1f;
    private float maxShootInterval = 4f;

    private WaitForSeconds intervalBetweenBullets = new WaitForSeconds(.1f);
    private Player player;

    void Start() {
        player = GetterUtility.GetPlayer();
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer() {
        while (true) {
            WaitForSeconds wfsShootInterval = new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            yield return wfsShootInterval;
            for (int i = 0; i < Random.Range(minBullets, maxBullets); i++) {
                EnemyBullet newEnemyBullet = Instantiate(bullet).GetComponent<EnemyBullet>();
                newEnemyBullet.ShootWithRay(GetShootRay(player.transform.position));
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
        Instantiate(enemyExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
