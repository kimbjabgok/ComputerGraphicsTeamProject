using UnityEngine;

public class DoorWithPassword : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public string correctPassword = "1234";

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(0f, openAngle, 0f) * closedRotation;
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openedRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public bool TryOpen(string input)
    {
        if (input == correctPassword)
        {
            isOpen = true;
            return true;
        }
        return false;
    }
}