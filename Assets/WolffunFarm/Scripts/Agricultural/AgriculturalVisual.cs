using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgriculturalVisual : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    public void SetText(string name, int product, TimeSpan time)
    {
        text.text = $"{name}\n{product}\n{UtilsClass.ConvertTimeSpanToMinusSecond(time)}";
    }
}
