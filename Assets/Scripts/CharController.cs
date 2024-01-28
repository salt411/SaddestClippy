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
        MoveInDirection();
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

    void MoveInDirection()
    {
        Vector3 forceDirection = movingLeft ? Vector3.left : Vector3.right;
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
        await Task.Delay(1000);
        isGametime = true;
    }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using UnityEngine;

//public class CharController : MonoBehaviour
//{
//    // Update is called once per frame[
//    //[SerializeField]
//    //private Vector3 lastPos;
//    private bool isGametime = false;
//    private GameObject timer;
//    private Timer timerScript;
//    private bool canMoveRight = false;
//    [SerializeField]
//    private float delayAfterCollision = 0.5f;
//    bool isStuck = false;
//    float movementTolerance = 0.01f;
//    void Start()
//    {
//        CheckTime();
//        timer = GameObject.Find("Timer");
//        timerScript = timer.GetComponent<Timer>();
//    }

//    void Update()
//    {


//        CheckForObstacle();

//    }

//    void CheckForObstacle()
//    {
//        Vector3 center = transform.position;
//        float originalHeight = 2f;
//        float originalRadius = 0.5f;
//        float scaledHeight = transform.localScale.y * originalHeight * 2f;
//        float scaledRadius = transform.localScale.x * originalRadius;

//        float heightOffset = scaledHeight / 2;

//        Vector3 point1 = center + Vector3.up * (scaledHeight / 2 - scaledRadius);// - heightOffset);
//        Vector3 point2 = center - Vector3.up * (scaledHeight / 2 - scaledRadius);// + heightOffset);

//        float radius = scaledRadius;
//        Vector3 direction = Vector3.right;
//        float maxDistance = 1f;
//        RaycastHit hit;


//        if (Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance))
//        {
//            if (hit.collider != null && isGametime && !jumping)
//            {
//                StartCoroutine(Jump());
//            }

//            Debug.DrawLine(point1, hit.point, Color.green);
//            Debug.DrawLine(point2, hit.point, Color.green);
//        }
//        else
//        {
//            // Draw a red line to represent the capsule when there is no hit
//            Debug.DrawLine(point1, point1 + direction * maxDistance, Color.red);
//            Debug.DrawLine(point2, point2 + direction * maxDistance, Color.red);
//        }
//    }
//    async void CheckTime()
//    {
//        await Task.Delay(1000);
//        isGametime = true;
//    }

//    void FixedUpdate()
//    {
//        //IsStuck();
//        MoveRight();

//    }
//    [SerializeField]
//    float force = 10f;
//    [SerializeField]
//    float horizontalForceWhileJumping = 1f;
//    [SerializeField]
//    float jumpForce = 10f;
//    [SerializeField]
//    float reboundForce = 2f;

//    void OnCollisionEnter(Collision other)
//    {
//        StartCoroutine(IsStuckChecker());
//    }

//    IEnumerator IsStuckChecker()
//    {
//        Vector3 startingPosition = transform.position;
//        float timeLeft = 3f;
//        while (timeLeft > 0)
//        {
//            yield return new WaitForSeconds(1f);
//            timeLeft--;

//            float diff = Mathf.Abs(transform.position.x - startingPosition.x);
//            if (diff >= 1)
//            {
//                yield break;
//            }
//        }
//        force = -force;
//        horizontalForceWhileJumping = -horizontalForceWhileJumping;
//    }
    


//    //void IsStuck()
//    //{
//    //    var distance = GetHorizontalDistance(lastPos) / Time.deltaTime;
//    //    lastPos = transform.position;
//    //    Rigidbody rb = GetComponent<Rigidbody>();
//    //    if (distance <= movementTolerance && rb.velocity.y < 0)
//    //    {
//    //        isStuck = true;
//    //    }
//    //    else
//    //    {
//    //        isStuck = false;
//    //    }
//    //}

//    //private void IfCollision(Collider other)
//    //{
//    //    GetComponent<Rigidbody>().AddForce(Vector3.left * reboundForce);
//    //    StartCoroutine(DelayRightMovement());

//    //}

//    //IEnumerator DelayRightMovement()
//    //{
//    //    canMoveRight = false;
//    //    yield return new WaitForSeconds(delayAfterCollision);
//    //    canMoveRight = true;
//    //}
//    private void MoveRight()
//    {
//        if (timerScript.levelWin == true)
//        {
//            GetComponent<Rigidbody>().AddForce(Vector3.right * 0);
//        }
//        else if (IsGrounded())
//        {
//            GetComponent<Rigidbody>().AddForce(Vector3.right * force);
//        }
//        else
//        {
//            GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalForceWhileJumping);
//        }
//}
//    bool IsGrounded()
//    {
//        float extraHeightText = 0.5f; // Small extra distance to account for uneven ground
//        RaycastHit hit;
//        Vector3 origin = transform.position + Vector3.up * 0.1f; // Slightly above the bottom of the character

//        if (Physics.Raycast(origin, Vector3.down, out hit, GetComponent<Collider>().bounds.extents.y + extraHeightText))
//        {
//            return true; // Grounded
//        }

//        return false; // Not grounded
//    }



//    bool jumping = false;

//    IEnumerator Jump()
//    {
//        if (!jumping)
//        {
//            jumping = true;

//            Rigidbody rb = GetComponent<Rigidbody>();
//            Vector3 velocity = rb.velocity;
//            velocity.y = 0;
//            rb.velocity = velocity;
//            jumping = true;
//            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

          
//            yield return new WaitForSeconds(0.5f);

//            jumping = false;
//        }
//    }



//    //public float GetHorizontalDistance(Vector3 compare)
//    //{
//    //    var distance = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(compare.x, 0, compare.z);
//    //    return distance.magnitude;
//    //}
//}