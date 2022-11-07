using UnityEngine;

public class LifeUI : MonoBehaviour
{
    public GameObject[] GOLives;

    void Start()
    {
        var pacman = FindObjectOfType<Life>();
        pacman.OnRemovedLife += Pacman_OnRemovedLife;
        UpdateLifesUI(pacman.GetComponent<Life>().Lives);
    }

    private void Pacman_OnRemovedLife(int remainingLives)
    {
        UpdateLifesUI(remainingLives);
    }

    private void UpdateLifesUI(int lives)
    {
        for (int i = 0; i < GOLives.Length; i++)
        {
            GOLives[i].SetActive(i < lives);
        }
    }
}
