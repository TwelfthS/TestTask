using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource stepSound;
    public GameObject tutorial;
    public GameObject hideBeforeStart;
    public GameObject hideAfterStart;
    public Animator animator;
    private Rigidbody rb;
    private float moveSpeed = 6f;
    private float maxHorizontalMovement = 10f;
    private float rotationDuration = 0.8f;
    private bool isRotating = false;
    private Quaternion targetRotation;
    private float rotationTime = 0f;
    private float stepTimeout;
    private bool isWalking = false;
    private bool finishedLevel = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isWalking && Input.touchCount > 0 && !finishedLevel)
        {
            isWalking = true;
            tutorial.SetActive(false);
            hideAfterStart.SetActive(false);
            hideBeforeStart.SetActive(true);
            animator.SetBool("isWalking", true);
        }
        if (isWalking)
        {
            Vector3 moveDirection = transform.position + transform.forward * Time.fixedDeltaTime * moveSpeed;
            stepTimeout -= Time.fixedDeltaTime;
            if (stepTimeout <= 0) {
                stepSound.Play();
                stepTimeout = 0.8f;
            }            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    float horizontalMovement = Mathf.Clamp(touch.deltaPosition.x, -maxHorizontalMovement, maxHorizontalMovement);
                    moveDirection += horizontalMovement * transform.right * Time.fixedDeltaTime;
                }
            }
            rb.MovePosition(moveDirection);
            if (isRotating) {
                rotationTime += Time.fixedDeltaTime;
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationTime / rotationDuration));
                rb.velocity = Vector3.zero;
                if (rotationTime >= rotationDuration)
                {
                    isRotating = false;
                    rotationTime = 0f;
                }
            }
        }
    }

    public void Turn(int direction)
    {
        isRotating = true;
        targetRotation = rb.rotation * Quaternion.Euler(0f, direction * 90f, 0f);
    }

    public void Stop() {
        isWalking = false;
        rb.velocity = Vector3.zero;
        finishedLevel = true;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Wall")) {
            rb.velocity = new Vector3(transform.forward.x * rb.velocity.x, transform.forward.y * rb.velocity.y, transform.forward.z * rb.velocity.z);
        }
    }
}
