using System;
using UnityEngine;

public static class UtilsClass 
{
    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static string TimeSpanToMinusSecondString(TimeSpan timeSpan)
    {
        return $"{timeSpan.Minutes}:{timeSpan.Seconds}";
    }

    public static float MinusToSecond(float minus)
    {
        return minus * 60;
    }

    public static string SecondToMinusSecondString(int second)
    {

        return $"{second / 60} : {second % 60}";
    }
}