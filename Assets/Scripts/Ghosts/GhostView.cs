using UnityEngine;

public enum GhostType
{
    Blinky,
    Pinky,
    Inky,
    Clyde
}
public class GhostView : MonoBehaviour
{
    public CharacterMotor CharacterMotor;
    public Animator Animator;
    public GhostAI GhostAI;
    public GhostType GhostType;
    void Start()
    {
        Animator.SetInteger("GhostType", (int)GhostType);
        CharacterMotor.OnDirectionChanged += CharacterMotor_OnDirectionChanged;
    }

    private void CharacterMotor_OnDirectionChanged(Direction direction)
    {
        Animator.SetInteger("Direction", (int)direction - 1);
    }
}
