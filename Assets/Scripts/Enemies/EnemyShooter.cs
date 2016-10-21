using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {

    public EnemyBullet bullet;

    public int Attackrange;
    public int minBullets = 1;
    public int maxBullets = 3;

    public Player target;
    public GameObject enemyExplosion;

    public float minShootInterval = 4f;
    public float maxShootInterval = 8f;

    private WaitForSeconds intervalBetweenBullets = new WaitForSeconds(.1f);
    private Player player;
    private SphereCollider attackRange;
    private float speed = 5f;
    public Vector3 flag = new Vector3(-55f, 0f, 0f);
    private Vector3 moveDirection;

    void Start() {
        player = GetterUtility.GetPlayer();;
        attackRange = gameObject.AddComponent<SphereCollider>();
        attackRange.radius = Attackrange;
        attackRange.isTrigger = true;
    }


    void FixedUpdate() {
        SearchTarget();
    }

    void SearchTarget()
    {
        OnTriggerEnter(attackRange);
        moveDirection = moveDirection.normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
        StartCoroutine(Shoot(target));
    }

    void OnTriggerEnter(Collider c)
    {
        
        if (c.gameObject.layer == LayerMask.NameToLayer("Player") || c.gameObject.layer == LayerMask.NameToLayer("Friend"))
        {
            moveDirection = c.gameObject.transform.position - transform.position;
        }
        else
        {
            target = null;
            moveDirection = flag - transform.position;
        }
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

    IEnumerator Shoot(Player target)
    {
        if (target != null)
        { 
            while (true)
            {
                WaitForSeconds wfsShootInterval = new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
                yield return wfsShootInterval;
                for (int i = 0; i < Random.Range(minBullets, maxBullets); i++)
                {
                    EnemyBullet newEnemyBullet = Instantiate(bullet).GetComponent<EnemyBullet>();
                    newEnemyBullet.ShootWithRay(GetShootRay(target.transform.position));
                    yield return intervalBetweenBullets;
                }
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
