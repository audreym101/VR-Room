using UnityEngine;

/// <summary>
/// Auto-player racket on the right side of the tennis court.
/// Serves balls toward the player and returns balls that come near it.
/// </summary>
public class TennisAutoPlayer : MonoBehaviour
{
    [Tooltip("Tennis ball prefab to spawn")]
    public GameObject ballPrefab;

    [Tooltip("Where balls are spawned/served from")]
    public Transform servePoint;

    [Tooltip("Target area on the player's (left) side to aim at")]
    public Transform playerSideTarget;

    [Tooltip("Seconds between each serve")]
    public float serveInterval = 4f;

    [Tooltip("Force applied when serving")]
    public float serveForce = 8f;

    [Tooltip("Force applied when returning a ball")]
    public float returnForce = 10f;

    [Tooltip("Radius around the racket to detect incoming balls")]
    public float detectionRadius = 1.5f;

    private float serveTimer;
    private bool waitingForReturn = false;

    void Start()
    {
        serveTimer = serveInterval;
    }

    void Update()
    {
        if (!waitingForReturn)
        {
            serveTimer -= Time.deltaTime;
            if (serveTimer <= 0f)
            {
                ServeBall();
                serveTimer = serveInterval;
                waitingForReturn = true;
            }
        }
        else
        {
            TryReturnNearbyBall();
        }
    }

    void ServeBall()
    {
        if (ballPrefab == null || servePoint == null || playerSideTarget == null) return;

        GameObject ball = Instantiate(ballPrefab, servePoint.position, Quaternion.identity);
        if (ball.TryGetComponent(out Rigidbody rb))
        {
            Vector3 direction = (playerSideTarget.position - servePoint.position).normalized;
            rb.AddForce(direction * serveForce, ForceMode.Impulse);
        }

        // Mark ball as served by auto-player
        if (ball.TryGetComponent(out TennisBallState state))
            state.servedByAutoPlayer = true;
    }

    void TryReturnNearbyBall()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out TennisBallState ball)) continue;
            if (!ball.servedByAutoPlayer) continue; // only return balls the player hit back
            if (ball.returned) continue;

            ball.returned = true;
            waitingForReturn = false;

            if (hit.TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                Vector3 direction = (playerSideTarget.position - transform.position).normalized;
                rb.AddForce(direction * returnForce, ForceMode.Impulse);
            }
            break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
