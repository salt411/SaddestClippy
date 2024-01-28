using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharController : MonoBehaviour
{
    // Update is called once per frame[
    //[SerializeField]
    //private Vector3 lastPos;
    private bool isGametime = false;
    private GameObject timer;
    private Timer timerScript;
    bool isStuck = false;
    float movementTolerance = 0.01f;
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

    void CheckForObstacle()
    {
        Vector3 center = transform.position;
        float originalHeight = 2f;
        float originalRadius = 0.5f;
        float scaledHeight = transform.localScale.y * originalHeight;
        float scaledRadius = transform.localScale.x * originalRadius;

        Vector3 point1 = center + Vector3.up * (scaledHeight / 2 - scaledRadius);
        Vector3 point2 = center - Vector3.up * (scaledHeight / 2 - scaledRadius);

        float radius = scaledRadius;
        Vector3 direction = Vector3.right;
        float maxDistance = 1f;
        RaycastHit hit;


        if (Physics.CapsuleCast(point1, point2, radius, direction, out hit, maxDistance))
        {
            if (hit.collider != null && isGametime)
            {
                Jump();
            }
        }
    }

    async void CheckTime()
    {
        await Task.Delay(1000);
        isGametime = true;
    }

    void FixedUpdate()
    {
        //IsStuck();
        MoveRight();

    }
    [SerializeField]
    float force = 10f;
    [SerializeField]
    float horizontalForceWhileJumping = 1f;
    [SerializeField]
    float jumpForce = 10f;

    //void IsStuck()
    //{
    //    var distance = GetHorizontalDistance(lastPos) / Time.deltaTime;
    //    lastPos = transform.position;
    //    Rigidbody rb = GetComponent<Rigidbody>();
    //    if (distance <= movementTolerance && rb.velocity.y < 0)
    //    {
    //        isStuck = true;
    //    }
    //    else
    //    {
    //        isStuck = false;
    //    }
    //}

    private void MoveRight()
    {


        if (timerScript.levelWin == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * 0);
        }
        else if (IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * force);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * horizontalForceWhileJumping);
        }

    }
    bool IsGrounded()
    {
        float extraHeightText = 0.5f; // Small extra distance to account for uneven ground
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up * 0.1f; // Slightly above the bottom of the character

        if (Physics.Raycast(origin, Vector3.down, out hit, GetComponent<Collider>().bounds.extents.y + extraHeightText))
        {
            return true; // Grounded
        }

        return false; // Not grounded
    }



    bool jumping = false;
    async void Jump()
    {
        if (!jumping)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            Vector3 velocity = rb.velocity;
            velocity.y = 0;
            jumping = true;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            await Task.Delay(700);
            jumping = false;
        }
    }


    //public float GetHorizontalDistance(Vector3 compare)
    //{
    //    var distance = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(compare.x, 0, compare.z);
    //    return distance.magnitude;
    //}
}