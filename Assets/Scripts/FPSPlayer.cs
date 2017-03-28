using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class FPSPlayer : MonoBehaviour {

    // Camera variables
    public Camera firstPersonCamera;
    Transform firstPersonCameraTransform;
    public float cameraHeight = 1.6f;
    public float mouseSensitivityX = 3f;
    public float mouseSensitivityY = 3f;
    public bool smoothMouse = true;
    public int mouseSmoothingFrames = 5;
    float[] mouseSmoothingX;
    float[] mouseSmoothingY;
    public float cameraTilt = 0.2f;
    public float fieldOfView = 80f;
    public float fieldOfViewDelta = 0f;
    public float FOVShiftSpeed = 10f;
    public float cameraMinAngle = -75f;
    public float cameraMaxAngle = 85f;
    float fovDeltaSmooth = 0f;
    public float mouseX = 0f;
    public float mouseY = 0f;
    public float mouseInputX = 0f;
    public float mouseInputY = 0f;
    float mouseYRotation = 0f;
    public bool invertMouseY = false;

    // Size variables
    public float colliderHeight = 1.8f;
    public float colliderRadius = 0.35f;
    CapsuleCollider myCollider;

    // Movement variables
    public float forwardsSpeed = 1.5f;
    public float sidewaysSpeedMultiplier = 0.8f;
    Transform myTransform;
    Rigidbody myRigidbody;

	void Start () {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!firstPersonCamera) firstPersonCamera = Camera.main;
        firstPersonCameraTransform = firstPersonCamera.transform;
        firstPersonCameraTransform.localPosition = Vector3.up * cameraHeight;
        firstPersonCamera.fieldOfView = fieldOfView;

        myCollider = GetComponent<CapsuleCollider>();
        myCollider.radius = colliderRadius;
        myCollider.height = colliderHeight;
        myCollider.center = Vector3.up * colliderHeight / 2;

        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();

        if (smoothMouse) {
            mouseSmoothingX = new float[mouseSmoothingFrames];
            mouseSmoothingY = new float[mouseSmoothingFrames];
        }
	}

    void FixedUpdate () {
        MouseInput();
        CameraUpdate();
        Movement();
	}

    void Update () {
        mouseInputX += Input.GetAxis("Mouse X");
        mouseInputY += Input.GetAxis("Mouse Y");
    }

    void MouseInput () {
        if (smoothMouse) {
            for (int i = mouseSmoothingFrames - 1; i > 0; --i) {
                mouseSmoothingX[i] = mouseSmoothingX[i - 1];
                mouseSmoothingY[i] = mouseSmoothingY[i - 1];
            }
            mouseSmoothingX[0] = mouseInputX * mouseSensitivityX;
            mouseSmoothingY[0] = mouseInputY * mouseSensitivityY;

            foreach (float input in mouseSmoothingX)
                mouseX += input;
            mouseX /= mouseSmoothingFrames;

            foreach (float input in mouseSmoothingY)
                mouseY += input;
            mouseY /= mouseSmoothingFrames;
        }

        else {
            mouseX = mouseInputX * mouseSensitivityX;
            mouseY = mouseInputY * mouseSensitivityY;
        }

        mouseX *= (1 / Time.fixedDeltaTime) / 60;
        mouseY *= (1 / Time.fixedDeltaTime) / 60;

        mouseInputX = 0;
        mouseInputY = 0;
    }

    void CameraUpdate () {
        if (invertMouseY)
            mouseY = -mouseY;
        myTransform.Rotate(myTransform.up * mouseX);
        mouseYRotation = Mathf.Clamp(mouseYRotation + mouseY, cameraMinAngle, cameraMaxAngle);
        firstPersonCameraTransform.localRotation = Quaternion.Euler((Vector3.left * mouseYRotation) + (Vector3.forward * mouseX * cameraTilt) + (Vector3.forward * -Input.GetAxis("Horizontal") * 3 * cameraTilt));
    }

    void Movement () {
        myRigidbody.velocity += myTransform.forward * forwardsSpeed * Input.GetAxis("Vertical") * ((1 / Time.fixedDeltaTime) / 60);
        myRigidbody.velocity += myTransform.right * forwardsSpeed * Input.GetAxis("Horizontal") * sidewaysSpeedMultiplier * ((1 / Time.fixedDeltaTime) / 60);
        //Debug.Log(myRigidbody.velocity);
    }
}
