using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public float interactRange = 3f;
    public TextMeshProUGUI indicatorText;

    void Update()
    {
        Camera cam = GetActiveCamera();
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        // "Interactable" 태그가 붙은 오브젝트만 감지
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                indicatorText.text = "!";
                return;
            }
        }

        indicatorText.text = "+";
    }

    Camera GetActiveCamera()
    {
        if (firstPersonCam != null && firstPersonCam.enabled) return firstPersonCam;
        if (thirdPersonCam != null && thirdPersonCam.enabled) return thirdPersonCam;
        return null;
    }
}