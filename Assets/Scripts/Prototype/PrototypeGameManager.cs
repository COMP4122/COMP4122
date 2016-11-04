using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeGameManager : MonoBehaviour {

	public GameObject HUDCanvas;
    public GameObject gameOverCanvas;

    private bool gameOver = false;

    public void EndGame() {
        HUDCanvas.SetActive(false);
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        gameOver = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && gameOver) {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level1");
        }
    }
}
