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
            Debug.LogError("PasswordUIManager�� ã�� �� �����ϴ�!");
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
                playerInteraction.canUseMouseInput = false; // ��й�ȣ �Է� �ÿ��� ��Ŭ�� �Է� �� ����
                uIManager.Show(door);
            } else
            {
                Debug.LogError("PlayerInteraction�� ã�� �� �����ϴ�!");
                return;
            }
        } 
        else if (isPasswordCleared)
        {
            print("�н������ �̹� Ǯ�Ƚ��ϴ�.");
        }
        else
        {
            Debug.LogError("PasswordUIManager�� ã�� �� �����ϴ�!");
        }
    }

    public void CheckPasswordCleared()
    {
        isPasswordCleared = true;
    }
}
