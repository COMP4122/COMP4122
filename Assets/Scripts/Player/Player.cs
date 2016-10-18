using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float health = 100f;
    public Text healthText;
    public Blade blade;

    private PlayerMovement playerMovement;
    private GameManager gameManager;

    void Start() {
        gameManager = GetterUtility.GetGameManager();
        playerMovement = GetComponent<PlayerMovement>();
        UpdateHealth();
    }

    //TODO: make a class for abilities
    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            blade.SetAwakeFor(0.5f);
        }
    }
    public void TakeDamage(float damageAmt) {
        health -= damageAmt;
        UpdateHealth();
        CheckDeath();
    }

    public void GetHitFrom(Vector3 position, float hitSpeed) {
        playerMovement.GetHitFrom(position, hitSpeed);
    }

    public void Die() {
        //TODO: implement this
        gameManager.EndGame();
    }

    void CheckDeath() {
        if (health <= 0) {
            Die();
        }
    }

    void UpdateHealth() {
        healthText.text = "Health: " + health;
    }

    #region getters
    public float GetHealth() {
        return health;
    }
    #endregion
}
