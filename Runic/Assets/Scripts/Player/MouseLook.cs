using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform camPosition;
    [SerializeField] private PlayerController pController;

    private float xRotation = 0f;
    private bool firstRotation = true;
    private Vector3 zIdlePreRotated;
    private Vector3 zAimPreRotated;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        zIdlePreRotated = new Vector3(spineTransform.localEulerAngles.x, spineTransform.localEulerAngles.y, spineTransform.localEulerAngles.z);
        initialPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        float dif = Mathf.Abs((transform.position.magnitude - camPosition.position.magnitude));
        if (pController.IsPlayerAiming()) {
            if (dif >= 0.1) {
                transform.position = Vector3.Lerp(transform.position, camPosition.position, 9f * Time.deltaTime);
            } else {
                transform.position = camPosition.position;
            }
        } else {
            if (dif >= 0.1) {
                transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, 9f * Time.deltaTime);
            } else {
                transform.localPosition = initialPos;
            }
        }
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

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
            spineTransform.localEulerAngles = new Vector3(0, spineTransform.localEulerAngles.y, xRotation);
        } else {
            spineTransform.localEulerAngles = zIdlePreRotated;
        }
    }
}
