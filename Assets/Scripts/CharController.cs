using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private bool isGametime = false;
    private GameObject timer;
    private Timer timerScript;
    private bool movingLeft = false;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float horizontalForceWhileJumping = 1f;
    [SerializeField]
    private float jumpForce = 10f;
    private bool jumping = false;
    private bool doneHere = false;

    void Start()
    {
        CheckTime();
        timer = GameObject.Find("Timer");
        timerScript = timer.GetComponent<Timer>();
    }

    void Update()
    {
        CheckForObstacle();
    }

    void FixedUpdate()
    {
        if (!doneHere)
        {
            MoveInDirection();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        StartCoroutine(IsStuckChecker());
    }

    IEnumerator IsStuckChecker()
    {
        Vector3 startPosition = transform.position;
        yield return new WaitForSeconds(2f); // Wait for 3 seconds
        Vector3 endPosition = transform.position;

        // Check if the movement in the x-direction is below a certain threshold
        if (Mathf.Abs(endPosition.x - startPosition.x) < 0.5f) // Threshold value can be adjusted
        {
            movingLeft = !movingLeft; // Invert the movement direction if not moved much
        }
    }

    public IEnumerator CheckTheTime()
    {
            doneHere = true;
            Debug.Log("YYYYYYYYY");
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
            }

            foreach (Transform child in transform)
            {
                Rigidbody childRb = child.GetComponent<Rigidbody>();
                if (childRb != null)
                {
                    childRb.useGravity = false;
                    childRb.velocity = Vector3.zero;
                }
            }

            // Wait for 1.5 seconds
            yield return new WaitForSeconds(1.5f);

            // Re-enable gravity
            if (rb != null)
            {
                rb.useGravity = true;
            }
            foreach (Transform child in transform)
            {
                Rigidbody childRb = child.GetComponent<Rigidbody>();
                if (childRb != null)
                {
                    childRb.useGravity = true;
                }
            }
        
    }
    void MoveInDirection()
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * 0); Vector3 forceDirection = movingLeft ? Vector3.left : Vector3.right;
            float currentForce = IsGrounded() ? force : horizontalForceWhileJumping;
            GetComponent<Rigidbody>().AddForce(forceDirection * currentForce);
        }
    

    bool IsGrounded()
    {
        float extraHeightText = 0.5f; // Small extra distance to account for uneven ground
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(origin, Vector3.down, out hit, GetComponent<Collider>().bounds.extents.y + extraHeightText))
        {
            return true; // Grounded
        }
        return false; // Not grounded
    }

    IEnumerator Jump()
    {
        if (!jumping)
        {
            jumping = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            jumping = false;
        }
    }

    void CheckForObstacle()
    {
        Vector3 center = transform.position;
        float originalHeight = 2f;
        float originalRadius = 0.5f;
        float scaledHeight = transform.localScale.y * originalHeight * 2f;
        float scaledRadius = transform.localScale.x * originalRadius;

        float heightOffset = scaledHeight / 2;

        Vector3 point1 = center + Vector3.up * (scaledHeight / 2 - scaledRadius);// - heightOffset);
        Vector3 point2 = center - Vector3.up * (scaledHeight / 2 - scaledRadius);// + heightOffset);

        float radius = scaledRadius;
        Vector3 direction = Vector3.right;
        float maxDistance = 1f;
        RaycastHit hit;


        if (Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance))
        {
            if (hit.collider != null && isGametime && !jumping)
            {
                StartCoroutine(Jump());
            }

            Debug.DrawLine(point1, hit.point, Color.green);
            Debug.DrawLine(point2, hit.point, Color.green);
        }
        else
        {
            // Draw a red line to represent the capsule when there is no hit
            Debug.DrawLine(point1, point1 + direction * maxDistance, Color.red);
            Debug.DrawLine(point2, point2 + direction * maxDistance, Color.red);
        }
    }
    async void CheckTime()
    {
        await Task.Delay(1500);
        isGametime = true;
    }
}


