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
        text.text = $"{name}\n{product}\n{UtilsClass.TimeSpanToMinusSecondString(time)}";
    }

    public void SetText(string name, int product, float second)
    {
        text.text = $"{name}\n{product}\n{UtilsClass.SecondToMinusSecondString((int)second)}";
    }

    public void SetText(string name, int product, float second, Color colorSecond)
    {
        text.text = $"{name}\n{product}\n<color=#{ColorUtility.ToHtmlStringRGBA(colorSecond)}>{UtilsClass.SecondToMinusSecondString((int)second)}</color>";
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
