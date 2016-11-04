using UnityEngine;
using UnityEngine.UI;

public class PrototypePlayer : MonoBehaviour {

    public float health = 100f;
    public Text healthText;
    public PrototypeBlade blade;

    private PrototypePlayerMovement playerMovement;
    private PrototypeGameManager gameManager;

    void Start() {
        gameManager = GetterUtility.GetGameManager();
        playerMovement = GetComponent<PrototypePlayerMovement>();
        UpdateHealth();
    }

    //TODO: make a class for abilities
    void Update() {
        if (Input.GetMouseButtonDown(2)) {
            blade.SetAwakeFor(0.5f);
        }
    }
    public void TakeDamage(float damageAmt) {
        health -= damageAmt;
        UpdateHealth();
        CheckDeath();
    }

    public void Heal(float healAmt) {
        health += healAmt;
        UpdateHealth();
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
