using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openedRotation = Quaternion.Euler(0f, openAngle, 0f) * closedRotation;
    }

    void Update()
    {
        // 문이 열린 상태면 열린 회전으로 보간
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openedRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
        }
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}