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

    private float xRotation = 0f; // ����
    private float yRotation = 0f; // �¿�

    void Start()
    {
        SetCameraView(true); // ������ 1��Ī
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

        // �÷��̾�� �¿� ȸ����
        playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);

        if (isFirstPerson)
        {
            firstPersonCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            // 3��Ī ī�޶� ȸ�� ��ġ ���
            Vector3 offset = Quaternion.Euler(xRotation, yRotation, 0f) * new Vector3(0f, 0f, -thirdPersonDistance);
            thirdPersonCam.transform.position = playerBody.position + Vector3.up * cameraHeightOffset + offset;
            thirdPersonCam.transform.LookAt(playerBody.position + Vector3.up * cameraHeightOffset);
        }
    }
}
