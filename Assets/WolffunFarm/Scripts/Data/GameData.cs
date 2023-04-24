using System.Collections.Generic;

public class GameData
{
    public int coint;
    public int levelDevice;
    public Dictionary<string, object> objectData;

    public GameData() 
    {
        objectData = new Dictionary<string, object>();
    }
}
