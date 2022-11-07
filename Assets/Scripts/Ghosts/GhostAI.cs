using UnityEngine;

[RequireComponent(typeof(GhostMove))]
public class GhostAI : MonoBehaviour
{
    private GhostMove _ghostMove;
    Transform _pacMan;



    private void Start()
    {
        _ghostMove = GetComponent<GhostMove>();
        _ghostMove.OnChangeTarget += GhostMove_OnChangeTarget;
        _pacMan = GameObject.FindWithTag("Player").transform;
    }

    private void GhostMove_OnChangeTarget()
    {
        _ghostMove.SetTargetPosition(_pacMan.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Life>().RemoveLife();
        }
    }
}
