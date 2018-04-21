using System;
using TMPro;
using UnityEngine;

public class MobilePhone : MonoBehaviour
{
    public TextMeshProUGUI RightAnswer;
    public TMPro.TMP_InputField InputField;

    public Transform Root;

    public MessageView HerMessagePrefab;
    public MessageView MyMessagePrefab;

    public event Action<string> MessageSent;
    public event Action<string> NewMessage;

    private void OnEnable()
    {
        InputField.Select();
        InputField.ActivateInputField();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var instance = Instantiate(MyMessagePrefab, Root, false);
            instance.Message.text = InputField.text;
            MessageSent?.Invoke(InputField.text);

            InputField.text = "";
        }
    }

    public void SentHerMessage(string message)
    {
        var instance = Instantiate(HerMessagePrefab, Root, false);
        instance.Message.text = message;
        NewMessage?.Invoke(message);
    }

    public void SetRightAnswer(string rightAnswer)
    {
        RightAnswer.text = rightAnswer;
    }
}