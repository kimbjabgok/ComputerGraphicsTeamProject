using UnityEngine;

public class PasswordPanelActivator : MonoBehaviour
{
    public GameObject panel;
    public Camera firstPersonCam;
    private bool isPasswordCleared;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PasswordUIManager uIManager = panel.GetComponent<PasswordUIManager>();

        if (uIManager != null )
        {
            uIManager.OperateStartSettings();
        } else
        {
            Debug.LogError("PasswordUIManager를 찾을 수 없습니다!");
            return;
        }

        isPasswordCleared = false;
    }

    public void ShowPasswordUI(DoorWithPassword door)
    {

        PasswordUIManager uIManager = panel.GetComponent<PasswordUIManager>();

        if (uIManager != null && !isPasswordCleared)
        {
            PlayerInteraction playerInteraction = firstPersonCam.GetComponent<PlayerInteraction>();
            if (playerInteraction != null) {
                playerInteraction.canUseMouseInput = false; // 비밀번호 입력 시에는 좌클릭 입력 안 받음
                uIManager.Show(door);
            } else
            {
                Debug.LogError("PlayerInteraction를 찾을 수 없습니다!");
                return;
            }
        } 
        else if (isPasswordCleared)
        {
            print("패스워드는 이미 풀렸습니다.");
        }
        else
        {
            Debug.LogError("PasswordUIManager를 찾을 수 없습니다!");
        }
    }

    public void CheckPasswordCleared()
    {
        isPasswordCleared = true;
    }
}
