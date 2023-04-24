using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Wolffun/Agricultural")]
public class AgriculturalSO : ScriptableObject 
{
    [Help("Make sure ID is same as object name", UnityEditor.MessageType.Warning)]
    public string ID;
    public int price;
    public int totalProduct;
    public float productTimeMinus;
    public AgriculturalVisual visualPrefab;
}
