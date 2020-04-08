using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private PlayerController pController;

    private float rotateMinimum = -25f;
    private float rotateMaximum = 25f;
    private float xRotation = 0f;
    private bool zRotated = false;
    private bool firstRotation = true;
    private Vector3 zIdlePreRotated;
    private Vector3 zAimPreRotated;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        zIdlePreRotated = new Vector3(spineTransform.localEulerAngles.x, spineTransform.localEulerAngles.y, spineTransform.localEulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        
        if (pController.IsPlayerAiming()) {
            xRotation = Mathf.Clamp(xRotation, rotateMinimum, rotateMaximum);
        } else {
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        }
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void LateUpdate()
    {
        if (pController.IsPlayerAiming()) {
            if (firstRotation) {
                zAimPreRotated = new Vector3(spineTransform.localEulerAngles.x, spineTransform.localEulerAngles.y, spineTransform.localEulerAngles.z);
                firstRotation = false;
            }
            //if (!zRotated) {
                //spineTransform.localEulerAngles = zAimPreRotated;
                //zRotated = true;
            //}
            spineTransform.localEulerAngles = new Vector3(0, spineTransform.localEulerAngles.y, xRotation);
        } else {
            spineTransform.localEulerAngles = zIdlePreRotated;
            //zRotated = false;
        }
    }
}
