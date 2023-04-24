using UnityEngine;

[CreateAssetMenu(fileName = "NewGame", menuName = "Wolffun/NewGame")]
public class NewGameSO : ScriptableObject
{
    public int plantedLand = 3;
    public int Wolker = 1;
    public int levelDevice = 1;
    public Seeds[] seeds;
}
