using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public MobilePhone MobilePhone;
    public NotificationPopup Prefab;
    public Transform Root;

    public void Start()
    {
        MobilePhone.NewMessage += OnNewMessage;
    }

    private void OnNewMessage(string message)
    {
        var popup = Instantiate(Prefab, Root, false);
        popup.Message.text = message;
    }
}