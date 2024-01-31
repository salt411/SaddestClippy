using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

public class UpgradedCharController : MonoBehaviour
{
    private GameObject timer;
    private Timer timerScript;
    private bool movingLeft = false;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float horizontalForceWhileJumping = 1f;
    [SerializeField]
    private float jumpForce = 0f;
    private bool midJump = false;
    private bool isGrounded = false;
    private bool doubleJumping = false;
    private Rigidbody rb;
    private int jumpCount;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForObstacle();
        if (jumpCount > 1)
        {
            IsGrounded();
        }
        //Debug.Log(jumpCount);
        //IsGrounded();
    }

    void FixedUpdate()
    {
        MoveInDirection();
    }

    void MoveInDirection()
    {
        Vector3 forceDirection = movingLeft ? Vector3.left : Vector3.right;
        float currentForce = isGrounded ? force : horizontalForceWhileJumping;
        GetComponent<Rigidbody>().AddForce(forceDirection * currentForce);
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
    void CheckForObstacle()
    {
        Vector3 center = transform.position;
        float originalHeight = 3f;
        float originalRadius = 0.7f;
        float scaledHeight = transform.localScale.y * originalHeight;
        float scaledRadius = transform.localScale.x * originalRadius;

        Vector3 point1 = center + Vector3.up * (scaledHeight / 2);
        Vector3 point2 = center - Vector3.up * (scaledHeight / 2);

        Vector3 direction = Vector3.right;
        float maxDistance = 5f;
        RaycastHit hit;


        if (Physics.CapsuleCast(point1, point2, scaledRadius, direction, out hit, maxDistance))
        {
            if (hit.collider != null)
            {
                StartCoroutine(Jump());
            }

            //Debug.DrawLine(point1, hit.point, Color.green);
            //Debug.DrawLine(point2, hit.point, Color.green);
        }
        else
        {
            // Draw a red line to represent the capsule when there is no hit
            //Debug.DrawLine(point1, point1 + direction * maxDistance, Color.red);
            //Debug.DrawLine(point2, point2 + direction * maxDistance, Color.red);
        }
    }

    void CheckThings()
    {
        float gravity = 9.8f;
        float maxHeight = (jumpForce * jumpForce) / (2 * gravity);
        float timeToMaxHeight = jumpForce / gravity;
        float totalFlightTime = 2 * timeToMaxHeight;
        float maxHorizontalRange = horizontalForceWhileJumping * totalFlightTime;
        float distanceToObject = 1.0f;
        //float distanceToObject = some formula dependent on storing the object
        float ratioOfRequiredForce = distanceToObject / maxHorizontalRange;

        Vector3 clippyPosition = transform.position;
        Vector3 forwardDirection = transform.forward;
        Vector3 upwardDirection = Vector3.up;

        RaycastHit hit;

        if (Physics.Raycast(clippyPosition, forwardDirection, out hit, maxHorizontalRange))
        {
            if (Physics.Raycast(clippyPosition, upwardDirection, out hit, maxHeight))
            {
                if (hit.collider != null)
                {
                    Debug.Log("obstacle found: " + hit.collider.gameObject.name);
                }
            }
        }



    }

    bool IsGrounded()
    {
        if (rb.velocity.y > 0)
        {
            isGrounded = false;
            return false;
        }
        float rightOffset = 0.1f;
        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.up * 0.1f + Vector3.right * rightOffset;
        float checkDistance = GetComponent<Collider>().bounds.extents.y;

        if (Physics.Raycast(origin, Vector3.down, out hit, GetComponent<Collider>().bounds.extents.y))
        {
            jumpCount = 0;
            Debug.Log("HIT");
            Debug.Log(jumpCount);
            Debug.DrawLine(origin, hit.point, Color.yellow);
            isGrounded = true;
            return true; // Grounded
        }
        Debug.DrawLine(origin, origin - Vector3.up * checkDistance, Color.red);
        isGrounded = false;
        return false; // Not grounded
    }
    

    IEnumerator Jump()
    {
        if (jumpCount < 2 && !midJump)
        {
            midJump = true;
            jumpCount++;
            Debug.Log(jumpCount);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            IsGrounded();
            midJump = false;

        }
    }


    
    

}
