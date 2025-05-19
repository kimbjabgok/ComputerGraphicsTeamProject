using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PasswordUIManager : MonoBehaviour
{
    public GameObject panel;
    public TMP_InputField inputField;
    public Button confirmButton;

    private DoorWithPassword currentDoor;

    public Camera firstPersonCam;

    public GameObject controller;

    void Start()
    {
        /*panel.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirm);*/
    }

    // 추가한 부분

    public void OperateStartSettings()
    {
        panel.SetActive(false);
    }

    //

    public void Show(DoorWithPassword door)
    {
        SetCursorLock(false);       // 마우스 커서 락 해제
        currentDoor = door;
        inputField.text = "";
        panel.SetActive(true);
        inputField.ActivateInputField();
        confirmButton.onClick.AddListener(OnConfirm);
    }

    void OnConfirm()
    {
        if (currentDoor.TryOpen(inputField.text))
        {
            Debug.Log("비밀번호 맞음. 문 열림!");
            panel.SetActive(false);
            SetCursorLock(true);
            PlayerInteraction playerInteraction 
                = firstPersonCam.GetComponent<PlayerInteraction>();
            if (playerInteraction != null) playerInteraction.ActivateMouseInput();
            else
            {
                Debug.LogError("PlayerInteraction를 찾을 수 없습니다!");
            }
            PasswordPanelActivator passwordPanelController
                    = controller.GetComponent<PasswordPanelActivator>();
            if (passwordPanelController != null) passwordPanelController.CheckPasswordCleared();
            else
            {
                Debug.LogError("PasswordPanelController를 찾을 수 없습니다!");
            }
        }
        else
        {
            Debug.Log("틀린 비밀번호.");
            inputField.text = "";
        }
    }

    public void SetCursorLock(bool lockCursor)
    {
        if (!lockCursor)
        {
            Cursor.lockState = CursorLockMode.None; // 커서 잠금 해제
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서 잠금 (원하는 경우)
        }
    }
}