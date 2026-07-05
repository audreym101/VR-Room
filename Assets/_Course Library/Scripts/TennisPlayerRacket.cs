using UnityEngine;

/// <summary>
/// Attach to the player's left-side tennis racket.
/// Detects when the racket hits a ball and notifies TennisBallState.
/// </summary>
public class TennisPlayerRacket : MonoBehaviour
{
    [Tooltip("Extra force multiplier applied in the hit direction")]
    public float hitForceMultiplier = 1.5f;

    private Rigidbody racketRb;

    void Awake()
    {
        racketRb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out TennisBallState ball)) return;

        ball.OnHitByPlayer();

        // Boost the ball in the direction it's already going + racket velocity
        if (collision.gameObject.TryGetComponent(out Rigidbody ballRb))
        {
            Vector3 hitDir = collision.relativeVelocity.normalized;
            if (racketRb != null)
                hitDir = (hitDir + racketRb.linearVelocity.normalized).normalized;

            ballRb.linearVelocity = hitDir * ballRb.linearVelocity.magnitude * hitForceMultiplier;
        }
    }
}
