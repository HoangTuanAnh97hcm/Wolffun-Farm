using UnityEngine;

[CreateAssetMenu(fileName = "Store", menuName = "Wolffun/Store")]
public class StoreSO : ScriptableObject
{
    public string ID;
    public int price;
    public int amount;
}
