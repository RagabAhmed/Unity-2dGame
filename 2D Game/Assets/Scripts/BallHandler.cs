using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] float detachDelay;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Rigidbody2D pivot;
    [SerializeField] float respawnDelay;

    private Rigidbody2D currentBallRigidBody;
    private SpringJoint2D currentSpringjoint;
    private Camera mainCamera;
    private bool isDragging;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall();
    }

    // Update is called once per frame
    void Update()
    {
        InputPhys();
    }

    private void InputPhys()
    {
        if (currentBallRigidBody == null) return;
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LaunchBall();
            }
            isDragging = false;
            return;
        }
        isDragging = true;
        currentBallRigidBody.isKinematic = true;
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
        currentBallRigidBody.position = worldPosition;
    }

    private void SpawnNewBall()
    {
        GameObject ballInstance=Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        currentBallRigidBody = ballInstance.GetComponent<Rigidbody2D>();
        currentSpringjoint = ballInstance.GetComponent<SpringJoint2D>();
        currentSpringjoint.connectedBody = pivot;
    }

    private void LaunchBall()
    {
        currentBallRigidBody.isKinematic = false;
        currentBallRigidBody = null;
        Invoke(nameof(DetachBall), detachDelay);
    }

    private void DetachBall()
    {
        currentSpringjoint.enabled = false;
        currentSpringjoint = null;
    }
    public void InitiateRespawn()
    {
        Invoke(nameof(SpawnNewBall), respawnDelay);
    }
}
