using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Starting,
        Playing,
        LifeLost,
        GameOver,
        Victory
    }

    public float StartupTime;

    public float LifeLostTimer;

    public event Action OnGameStarting;
    public static event Action OnVictory;
    public event Action OnGameOver;


    private float _lifeLostTimer;
    private bool _isGameOver;

    private GhostMove[] _allGhostMotors;
    private CharacterMotor _pacmanMotor;

    private GameState _gameState;
    private int _forVictoryCount;

    private void Start()
    {
        var allCollectibles = FindObjectsOfType<Collectible>();

        _forVictoryCount = 0;

        foreach (var collectible in allCollectibles)
        {
            _forVictoryCount++;
            collectible.OnCollected += Collectible_OnCollected;
        }

        _gameState = GameState.Starting;

        var pacman = GameObject.FindWithTag("Player");
        _allGhostMotors = FindObjectsOfType<GhostMove>();
        _pacmanMotor = pacman.GetComponent<CharacterMotor>();
        StopAllChars();

        pacman.GetComponent<Life>().OnRemovedLife += Pacman_OnRemovedLife;

    }

    private void Pacman_OnRemovedLife(int remainingLives)
    {
        StopAllChars();

        _lifeLostTimer = LifeLostTimer;
        _gameState = GameState.LifeLost;

        _isGameOver = remainingLives <= 0;
    }

    private void Update()
    {
        switch (_gameState)
        {
            case GameState.Starting:
                StartupTime -= Time.deltaTime;
                if (StartupTime <= 0)
                {
                    _gameState = GameState.Playing;
                    StartAllChars();
                    OnGameStarting?.Invoke();
                }
                break;

            case GameState.Playing:
                break;

            case GameState.LifeLost:
                _lifeLostTimer -= Time.deltaTime;

                if (_lifeLostTimer <= 0)
                {
                    if (_isGameOver)
                    {
                        _gameState = GameState.GameOver;
                        OnGameOver?.Invoke();
                    }
                    else
                    {
                        _gameState = GameState.Playing;
                        ResetAllChars();
                    }
                }
                break;

            case GameState.GameOver:

            case GameState.Victory:
                if (Input.anyKey)
                {
                    SceneManager.LoadScene(0);
                }
                StopAllChars();
                OnVictory?.Invoke();
                break;

            default:
                break;
        }
    }

    private void Collectible_OnCollected(int _, Collectible collectible)
    {
        _forVictoryCount--;
        if (_forVictoryCount <= 0)
        {
            Debug.Log("Win!");
            _gameState = GameState.Victory;
        }

        collectible.OnCollected -= Collectible_OnCollected;
    }

    private void ResetAllChars()
    {
        _pacmanMotor.ResetPosition();
        foreach (var ghost in _allGhostMotors)
        {
            ghost.ResetPos();
        }

        StartAllChars();
    }
    private void StartAllChars()
    {
        _pacmanMotor.enabled = true;
        foreach (var ghost in _allGhostMotors)
        {
            ghost.StartMoving();
        }
    }

    private void StopAllChars()
    {
        _pacmanMotor.enabled = false;
        foreach (var ghost in _allGhostMotors)
        {
            ghost.StopMoving();
        }
    }


}
