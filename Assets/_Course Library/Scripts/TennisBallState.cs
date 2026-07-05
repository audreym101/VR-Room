using UnityEngine;

/// <summary>
/// Tracks the state of a tennis ball during a rally.
/// </summary>
public class TennisBallState : MonoBehaviour
{
    [HideInInspector] public bool servedByAutoPlayer = false;
    [HideInInspector] public bool returned = false;

    [Tooltip("Auto-destroy ball after this many seconds")]
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Call this from the player's racket OnCollisionEnter to mark the ball as returned.
    /// </summary>
    public void OnHitByPlayer()
    {
        returned = false;        // reset so auto-player can return it again
        servedByAutoPlayer = true;
    }
}
