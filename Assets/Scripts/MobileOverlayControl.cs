using UnityEngine;

public class MobileOverlayControl : MonoBehaviour
{
    public MobilePhone MobilePhone;

    private void Update()
    {
        if (!MobilePhone.PlayerHealth.isDead && Input.GetKeyDown(KeyCode.Tab))
        {
            MobilePhone.gameObject.SetActive(!MobilePhone.gameObject.activeSelf);
        }
    }
}