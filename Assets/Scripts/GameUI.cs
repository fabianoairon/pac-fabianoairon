using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject ReadyMessage;
    public GameObject GameOverMessage;
    public AudioSource AudioSource;
    public AudioClip BeginningMusic;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.OnGameStarting += _gameManager_OnGameStarting;
        _gameManager.OnGameOver += _gameManager_OnGameOver;
        AudioSource.PlayOneShot(BeginningMusic);
        GameOverMessage.SetActive(false);
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
