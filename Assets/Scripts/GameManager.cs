using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currentStrokes = 0;
    private int maxStrokes = 10;
    
    private bool gameOver = false;
    
    public TMP_Text strokesText;
    public GameObject winScreen;
    public GameObject loseScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateStrokesUI();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void UpdateStrokesUI()
    {
        if (strokesText != null)
            strokesText.text = "Strokes: " + currentStrokes + " / " + maxStrokes + "";
    }

    public void AddStroke()
    {
        if (gameOver) return;
        
        currentStrokes++;
        UpdateStrokesUI();
        
        if (currentStrokes > maxStrokes)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        gameOver = true;
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }

    public void WinGame()
    {
        if (gameOver) return;
        
        Time.timeScale = 0f;
        gameOver = true;
        winScreen.SetActive(true);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
