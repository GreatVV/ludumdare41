using UnityEngine;

public class MobileOverlayControl : MonoBehaviour
{
    public MobilePhone MobilePhone;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MobilePhone.gameObject.SetActive(!MobilePhone.gameObject.activeSelf);
        }
    }
}