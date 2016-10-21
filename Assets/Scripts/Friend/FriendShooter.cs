using UnityEngine;
using System.Collections;

public class FriendShooter : MonoBehaviour {

    public BasicBullet bullet;

    public int Attackrange;
    public int minBullets = 1;
    public int maxBullets = 3;

    public GameObject friendExplosion;

    public float minShootInterval = 4f;
    public float maxShootInterval = 8f;

    private WaitForSeconds intervalBetweenBullets = new WaitForSeconds(.1f);
    private Player player;
    private Flag flag2;
    public Vector3 flag = new Vector3(55f,0f,0f);

    private float speed = 5f;

    void Start()
    {
        //flag  = GetterUtility.GetEnemy();
        SphereCollider attackRange = gameObject.AddComponent<SphereCollider>();
        attackRange.radius = Attackrange;
        attackRange.isTrigger = true;
        OnTriggerEnter(attackRange);
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(ShootFlag());
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = flag - transform.position;
        moveDirection = moveDirection.normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }


    IEnumerator ShootFlag()
    {
        while (true)
        {
            WaitForSeconds wfsShootInterval = new WaitForSeconds(Random.Range(minShootInterval, maxShootInterval));
            yield return wfsShootInterval;
            for (int i = 0; i < Random.Range(minBullets, maxBullets); i++)
            {
                BasicBullet newFriendBullet = Instantiate(bullet).GetComponent<BasicBullet>();
                newFriendBullet.ShootWithRay(GetShootRay(flag));
                yield return intervalBetweenBullets;
            }

        }
    }

    private Ray GetShootRay(Vector3 targetPosition)
    {
        Vector3 shootDirection = targetPosition - transform.position;
        Ray shootRay = new Ray(transform.position + 1f * shootDirection.normalized, shootDirection);
        return shootRay;
    }

    public void Die()
    {
        Instantiate(friendExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
