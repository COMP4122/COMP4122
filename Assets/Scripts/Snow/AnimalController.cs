using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour {
    public float maxHealth, walkSpeed, runSpeed, detectDistance;
    public bool isAggresive;
    public float hitRate, hitDamage, timeBeforeEscape;
    public GameObject meat;
    public AudioClip walkAudio, roarAudio, runAudio, attackAudio, hurtAudio, dieAudio;

    private Camera mainCamera;
    private Transform player, targetLocation;
    private PlayerStateController playerController;
    private Rigidbody rb;
    private Collider co;
    private Animator anim;
    private NavMeshAgent nav;
    private GameObject[] navPoints;
    private AudioSource audioSource;

    private float nextHit, health, hpRatio;
    private bool die = false, isHitting = false, hitted = false, isroaring = false, alert = false, escaping = false;
    
	void Start () {
        // Find Player
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null) {
            player = playerObject.transform;
            playerController = playerObject.GetComponent<PlayerStateController>();
        }
        
        // Setup Component
        mainCamera = Camera.main;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        co = GetComponentInChildren<MeshCollider>();
        nav = GetComponent<NavMeshAgent>();
        navPoints = GameObject.FindGameObjectsWithTag("Navigation");
        audioSource = GetComponent<AudioSource>();

        // Initialize variables
        nextHit = 0.0f;
        health = maxHealth;
    }
	
	void FixedUpdate () {
        if (!die)
        {
            // Calculate health to determine whether to die
            if (health <= 0.0f) {
                die = true;
                StartCoroutine(Die());
                return;
            }

            // This animal has found player
            if (alert)
            {
                if (isAggresive)
                {
                    if (Time.time > nextHit && Vector3.Distance(transform.position, player.position) < 4.0f)
                        Hit();
                    if (!isHitting && Vector3.Distance(transform.position, player.position) > 3.5f)
                        RunToPlayer();
                    if (!isHitting && Vector3.Distance(transform.position, player.position) <= 3.5f)
                        WaitToHit();
                }
                else {
                    AvoidPlayer();
                    if (escaping) {
                        Vector3 position = mainCamera.WorldToViewportPoint(transform.position);
                        if ((position.x < 0.0f || position.x > 1.0f) && (position.y < 0.0f || position.y > 1.0f) && Vector3.Distance(transform.position, player.transform.position) > 20.0f)
                            Destroy(gameObject);
                    }
                }
            }

            // This animal hasn't found player
            else
            {
                // Walk randomly
                if ((targetLocation == null || Vector3.Distance(transform.position, targetLocation.position) < 3.0f) && !isroaring)
                    patrol();

                // Find player
                if (Vector3.Distance(transform.position, player.position) < detectDistance || isroaring)
                {
                    if (isAggresive)
                    {
                        if (!isroaring)
                            StartCoroutine(NoticePlayer());
                        else
                            RotateToPlayer(0.05f);
                    }
                    else {
                        alert = true;
                        StartCoroutine(CountBeforeEscape());
                    }
                }
            }
        }
        else {
            transform.Rotate(0.0f, 0.0f, 0.5f);
        }
    }

    void Hit() {
        audioSource.clip = attackAudio;
        audioSource.loop = false;
        audioSource.Play();
        isHitting = true;
        anim.SetTrigger("Hit");
        nextHit = Time.time + hitRate;
        StartCoroutine(FinishHit());
    }

    void RunToPlayer() {
        if (!audioSource.isPlaying && audioSource.clip != runAudio) {
            audioSource.loop = true;
            audioSource.clip = runAudio;
            audioSource.Play();
        }
        SetRunning(true);
        nav.destination = player.position;
    }

    void WaitToHit() {
        if (!audioSource.isPlaying && audioSource.clip != runAudio) {
            audioSource.loop = true;
            audioSource.clip = runAudio;
            audioSource.Play();
        }
        if (anim.GetFloat("Speed") > 0.1f)
            anim.SetFloat("Speed", 0.0f);
    }

    void AvoidPlayer() {
        if (audioSource.clip != runAudio) {
            audioSource.loop = true;
            audioSource.clip = runAudio;
            audioSource.Play();
        }
        SetRunning(true);
        if (Vector3.Distance(transform.position, targetLocation.transform.position) > 5.0f)
            return;
        float maxDistance = 0.0f;
        int index = 0;
        for (int i = 0; i < navPoints.Length; i++) {
            float distance = Vector3.Distance(player.transform.position, navPoints[i].transform.position);
            if (distance > maxDistance) {
                maxDistance = distance;
                index = i;
            }
        }
        nav.destination = navPoints[index].transform.position;
        targetLocation = navPoints[index].transform;
    }

    void patrol() {
        if (audioSource.clip != walkAudio) {
            audioSource.loop = true;
            audioSource.clip = walkAudio;
            audioSource.Play();
        }
        SetRunning(false);
        int index = Mathf.FloorToInt(Random.Range(0.0f, navPoints.Length));
        nav.destination = navPoints[index].transform.position;
        targetLocation = navPoints[index].transform;
    }

    // This function is called by aggressive animal to process attack duration and avoid multiple damage in one attack
    IEnumerator FinishHit() {
        yield return new WaitForSeconds(0.6f);
        isHitting = false;
        hitted = false;
    }

    /* This function is called when animal dies
     * It lets the animal fall into the ground and spawn the meat.
     */
    IEnumerator Die() {
        // Play die animation and audio
        anim.SetTrigger("Die");
        audioSource.clip = dieAudio;
        audioSource.loop = false;
        audioSource.Play();
        nav.enabled = false;
        rb.velocity = Vector3.zero;

        // Disable UI and physics to let animal fall into the ground, and spawn meat
        yield return new WaitForSeconds(3);
        anim.enabled = false;
        audioSource.enabled = false;
        rb.velocity = Vector3.down * 1.0f;
        co.enabled = false;
        rb.useGravity = false;
        Instantiate(meat, transform.position, Quaternion.identity);

        // Wait for 10 seconds and destroy this object
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    // This function is used by non-aggresive animal to count down seconds before it can run out of scene
    IEnumerator CountBeforeEscape() {
        yield return new WaitForSeconds(timeBeforeEscape);
        escaping = true;
    }

    // This function is used by aggresive animal when its attack touches player
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && isHitting && !hitted) {
            hitted = true;
            playerController.TakeDamage(hitDamage);
        }
    }

    // This function is to detect that player's weapon hits the animal
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Arrow") {
            ArrowController ac = collision.gameObject.GetComponent<ArrowController>();
            float ratio = collision.relativeVelocity.magnitude / ac.maxSpeed;
            TakeDamage(ac.maxDamage * ratio);
        }
    }

    /* This function is called by aggressive animal when it finds player.
       The animal shall roar before try to attack.
       The animation and audio for roaring should be around 4 seconds.
    */
    IEnumerator NoticePlayer() {
        // Start roaring
        nav.enabled = false;
        isroaring = true;
        anim.SetTrigger("Angry");

        // Play roar audio
        audioSource.loop = false;
        audioSource.clip = roarAudio;
        audioSource.PlayDelayed(0.5f);

        // Wait to finish roaring
        yield return new WaitForSeconds(4);
        alert = true;
        isroaring = false;
        nav.enabled = true;
        audioSource.loop = true;
    }

    /* This function is called when animal is attacked by player
     * */
    void TakeDamage(float damage) {
        if (health > 0) {
            audioSource.clip = hurtAudio;
            audioSource.loop = false;
            audioSource.Play();
            if (damage > health)
                damage = health;
            health -= damage;
        }

        // If the animal hasn't noticed player, now it does
        if (!alert) {
            if (isAggresive && !isroaring)
                StartCoroutine(NoticePlayer());
            else if (!isAggresive) {
                alert = true;
                StartCoroutine(CountBeforeEscape());
            }
        }
    }

    /* This function is to set animal move method.
     * If argument is true, the animal shall run, else the animal shall walk.
     * */
    void SetRunning(bool run) {
        if (run) {
            nav.speed = runSpeed;
            anim.SetFloat("Speed", 5.0f);
        }
        else {
            nav.speed = walkSpeed;
            anim.SetFloat("Speed", 1.0f);
        }
    }

    // To let animal turns to player gradually
    void RotateToPlayer(float speed) {
        Quaternion originalRotation = transform.rotation;
        transform.LookAt(player);
        Quaternion targetRotation = transform.rotation;
        transform.rotation = originalRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed);
    }
}
