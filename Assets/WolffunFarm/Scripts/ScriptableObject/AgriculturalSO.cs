using UnityEngine;

[CreateAssetMenu(menuName = "Wolffun/Agricultural")]
public class AgriculturalSO : ScriptableObject 
{
    public string nameString;
    public int price;
    public int totalProduct;
    public float productTimeMinus;
    public AgriculturalVisual visualPrefab;
}
