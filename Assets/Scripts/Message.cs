using System;
using UnityEngine;

[Serializable]
public class Message
{
    [TextArea (12, 3000)]
    public string Text;
    [TextAreaAttribute (12, 3000)]
    public string[] Options;
}