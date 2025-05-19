using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera thirdPersonCam;

    public Transform playerBody;

    public float thirdPersonDistance = 4f;
    public float cameraHeightOffset = 1.5f;

    public float mouseSensitivity = 100f;

    private bool isFirstPerson = true;

    private float xRotation = 0f; // 상하
    private float yRotation = 0f; // 좌우

    void Start()
    {
        SetCameraView(true); // 시작은 1인칭
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isFirstPerson = !isFirstPerson;
            SetCameraView(isFirstPerson);
        }

        RotateWithMouse();
    }

    void SetCameraView(bool firstPerson)
    {
        firstPersonCam.enabled = firstPerson;
        thirdPersonCam.enabled = !firstPerson;
    }

    void RotateWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        // 플레이어는 좌우 회전만
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (isFirstPerson)
        {
            firstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            // 3인칭 카메라 회전 위치 계산
            Vector3 offset = Quaternion.Euler(xRotation, yRotation, 0f) * new Vector3(0f, 0f, -thirdPersonDistance);
            thirdPersonCam.transform.position = playerBody.position + Vector3.up * cameraHeightOffset + offset;
            thirdPersonCam.transform.LookAt(playerBody.position + Vector3.up * cameraHeightOffset);
        }
    }
}
