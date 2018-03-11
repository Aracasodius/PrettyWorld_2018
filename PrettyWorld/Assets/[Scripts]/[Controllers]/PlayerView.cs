using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Transform playerBody;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float focusZoomAmount = 10f;

    public bool focused;
    public bool lockView;

    float xAxisClamp = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateCamera();
    }

    private void FixedUpdate()
    {
        #region Interactable Region

        Ray ray = Camera.main.ScreenPointToRay(new Vector2 (Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, hit.transform.position) < interactable.radius)
                {
                    Cursor.lockState = CursorLockMode.None;
                    FocusOnObject(hit.transform, focusZoomAmount);
                    interactable.Interact();
                }
            }
        }

        #endregion
    }

    void RotateCamera()
    {
        if (!lockView)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            float rotAmountX = mouseX * mouseSensitivity;
            float rotAmountY = mouseY * mouseSensitivity;

            xAxisClamp -= rotAmountY;

            Vector3 targetRotCam = transform.rotation.eulerAngles;
            Vector3 targetRotBody = playerBody.rotation.eulerAngles;

            targetRotCam.x -= rotAmountY;
            targetRotCam.z = 0;
            targetRotBody.y += rotAmountX;

            if (xAxisClamp > 90) { xAxisClamp = targetRotCam.x = 90; }
            else if (xAxisClamp < -90) { xAxisClamp = -90; targetRotCam.x = 270; }

            transform.rotation = Quaternion.Euler(targetRotCam);
            playerBody.rotation = Quaternion.Euler(targetRotBody);
        }
    }

    void FocusOnObject(Transform newFocus, float zoomamount)
    {
        if (focused)
        {
            RemoveFocusOnObject(focusZoomAmount);
            lockView = false;
        }
        else
        {
            Camera.main.fieldOfView -= zoomamount;
            focused = true;
            lockView = true;
        }
    }

    public void RemoveFocusOnObject(float zoomamount)
    {
        Cursor.lockState = CursorLockMode.Locked;
        focused = false;
        lockView = false;
        Camera.main.fieldOfView += zoomamount;
    }

    public void RemoveFocusOnObject()
    {
        RemoveFocusOnObject(focusZoomAmount);
    }

    public void LockCamera(bool Locking)
    {
        if (!Locking)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Locking)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        lockView = (!lockView);
    }
}
