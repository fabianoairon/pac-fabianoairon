using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject ReadyMessage;
    public GameObject GameOverMessage;
    public AudioSource AudioSource;
    public AudioClip BeginningMusic;
    public BlinkTilemapColor BlinkTilemapColor;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnGameStarting += _gameManager_OnGameStarting;
        _gameManager.OnGameOver += _gameManager_OnGameOver;
        GameManager.OnVictory += GameManager_OnVictory;
        AudioSource.PlayOneShot(BeginningMusic);
        GameOverMessage.SetActive(false);
    }

    private void GameManager_OnVictory()
    {
        if (BlinkTilemapColor == null) return;
        BlinkTilemapColor.enabled = true;
    }

    private void _gameManager_OnGameOver()
    {
        GameOverMessage.SetActive(true);
    }

    private void _gameManager_OnGameStarting()
    {
        ReadyMessage.SetActive(false);
    }
}
