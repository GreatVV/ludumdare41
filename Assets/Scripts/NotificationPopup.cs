using ProBuilder.Core;
using TMPro;
using UnityEngine;

public class NotificationPopup : MonoBehaviour
{
    public TextMeshProUGUI Message;
    public float LifeTime = 10f;
    private float _timeLeft;

    void Start()
    {
        _timeLeft = LifeTime;
    }

    void Update()
    {
        if (_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}