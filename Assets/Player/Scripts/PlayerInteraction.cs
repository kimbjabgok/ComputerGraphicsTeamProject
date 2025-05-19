using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public float interactRange = 5f;

    public bool canUseMouseInput;

    void Start()
    {
        canUseMouseInput = true;
    }

    void Update()
    {
        if (canUseMouseInput)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("좌클릭 감지됨");

                Camera cam = GetActiveCamera();
                if (cam == null)
                {
                    Debug.LogWarning("활성화된 카메라가 없습니다. (둘 다 꺼져 있음 또는 연결되지 않음)");
                    return;
                }

                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red, 1.0f);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    Debug.Log("충돌된 오브젝트 이름: " + hit.collider.name);
                    Debug.Log("해당 태그: " + hit.collider.tag);

                    // ✅ 아이템 태그는 상호작용 대상에서 제외
                    if (hit.collider.CompareTag("Item"))
                    {
                        Debug.Log("아이템은 상호작용 대상이 아님. 무시합니다.");
                        return;
                    }

                    if (hit.collider.CompareTag("Interactable"))
                    {
                        DoorWithPassword passwordDoor = hit.collider.GetComponentInParent<DoorWithPassword>();
                        if (passwordDoor != null)
                        {
                            Debug.Log("📌 비밀번호 문 감지됨!");

                            var passwordActivator = FindFirstObjectByType<PasswordPanelActivator>();

                            if (passwordActivator == null)
                            {
                                Debug.LogError("❌ PasswordPanelActivator를 찾을 수 없습니다!");
                                return;
                            }

                            passwordActivator.ShowPasswordUI(passwordDoor);
                            return;
                        }

                        Door door = hit.collider.GetComponent<Door>();
                        if (door != null)
                        {
                            door.ToggleDoor();
                            Debug.Log("문 상태 토글!");
                        }
                        else
                        {
                            Debug.LogError("Door를 찾을 수 없습니다!");
                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("이 오브젝트는 Interactable 태그가 아닙니다.");
                    }
                }
                else
                {
                    Debug.Log("Ray가 아무 오브젝트도 감지하지 못함.");
                }
            }
        }
    }

    // SetActive 기반으로 활성 카메라 판별
    Camera GetActiveCamera()
    {
        if (firstPersonCam != null && firstPersonCam.gameObject.activeSelf)
            return firstPersonCam;

        if (thirdPersonCam != null && thirdPersonCam.gameObject.activeSelf)
            return thirdPersonCam;

        return null;
    }

    public void ActivateMouseInput()
    {
        canUseMouseInput = true;
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