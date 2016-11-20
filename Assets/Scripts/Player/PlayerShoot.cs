using System.Collections;
using UnityEngine;

public enum ShootMode { Rock, Bow }

public class PlayerShoot : MonoBehaviour{

    public GameObject mainCamera;

    public GameObject rockInHand;
    public Projectile rock;
    public GameObject bowInHand;
    public GameObject arrowInHand;
    public Projectile arrow;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private float shootSpeed = 0f;
    private float maxShootSpeed = 120f;
    private float shootSpeedIncreaseRate = 50f;

    private Animator animator;
    private bool shooting = false;
    
    public ShootMode shootMode = ShootMode.Bow;

    void Start() {
        animator = GetComponent<Animator>();
        rockInHand.SetActive(false);
        bowInHand.SetActive(false);
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && !shooting) {
            shooting = true;
            switch (shootMode) {
                case ShootMode.Rock:
                    rockInHand.SetActive(true);
                    animator.SetTrigger("ThrowRock");

                    break;
                case ShootMode.Bow:
                    bowInHand.SetActive(true);
                    arrowInHand.SetActive(true);
                    animator.SetTrigger("StrechBow");

                    audioSource.clip = audioClips[0];
                    audioSource.Play();

                    break;
            }
        }

        if (shooting) {
            ShootingMode();
        }
    }

    void ShootingMode() {
        if (Input.GetMouseButton(0)) {
            if (shootSpeed < maxShootSpeed)
                shootSpeed += shootSpeedIncreaseRate * Time.deltaTime;
            if (shootSpeed >= maxShootSpeed)
                switch (shootMode) {
                    case ShootMode.Rock:
                        animator.SetTrigger("HoldRock");
                        break;
                    case ShootMode.Bow:
                        animator.SetTrigger("HoldBow");
                        break;
                }
        }

        if (Input.GetMouseButton(0) == false){
            switch (shootMode) {
                case ShootMode.Rock:
                    StartCoroutine(ThrowOutRockCoroutine());
                    break;
                case ShootMode.Bow:
                    StartCoroutine(ShootOutArrowCoroutine());
                    break;
            }

            shooting = false;
            shootSpeed = 0f;
        }
    }

    IEnumerator ThrowOutRockCoroutine() {
        animator.SetTrigger("ThrowOutRock");
        Vector3 posForRock = rockInHand.transform.position;
        rockInHand.SetActive(false);
        Vector3 target = transform.position + mainCamera.transform.forward * 50f;

        Projectile newRock = (Projectile) Instantiate(rock, posForRock, new Quaternion(0f, 0f, 0f, 0f));
        newRock.SetFlySpeed(shootSpeed);
        newRock.SetTarget(target);
        newRock.ThrowOut();
        newRock.damage = shootSpeed;

        yield return new WaitForSeconds(0.2f);
        CancelShoot();
        StopCoroutine("ThrowOutRockCoroutine");
    }

    IEnumerator ShootOutArrowCoroutine() {
        animator.SetTrigger("ShootOutArrow");
        Vector3 posForArrow = arrowInHand.transform.position;
        arrowInHand.SetActive(false);
        Vector3 target = arrowInHand.transform.position + arrowInHand.transform.up * 50f;

        Projectile newArrow = (Projectile) Instantiate(arrow, posForArrow, new Quaternion(0f, 0f, 0f, 0f));
        newArrow.transform.position = arrowInHand.transform.position;
        newArrow.transform.rotation = arrowInHand.transform.rotation;
        newArrow.SetFlySpeed(shootSpeed);
        newArrow.SetTarget(target);
        newArrow.ThrowOut();
        newArrow.damage = shootSpeed;

        audioSource.clip = audioClips[1];
        audioSource.Play();

        yield return new WaitForSeconds(0.2f);
        CancelShoot();
        StopCoroutine("ShootOutArrowCoroutine");
    }

    void CancelShoot() {
        animator.SetTrigger("CancelShoot");
        shooting = false;
        shootSpeed = 0f;

        switch (shootMode) {
            case ShootMode.Rock:
                rockInHand.SetActive(false);
                break;
        }
    }

	public void SwitchToBow() {
		shooting = false;
		shootSpeed = 0f;
		maxShootSpeed = 120f;
		shootMode = ShootMode.Bow;
		bowInHand.SetActive (true);
	}

	public void SwitchToRock() {
		shooting = false;
		shootSpeed = 0f;
		maxShootSpeed = 50f;

		shootMode = ShootMode.Rock;
	}

}
