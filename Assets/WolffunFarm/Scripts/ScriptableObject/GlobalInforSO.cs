using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wolffun/GlobalInfor")]
public class GlobalInforSO : ScriptableObject
{
    [Tooltip("Speed Of Game")]
    public float timeScale = 1f;

    [Header("In-game stats")]

    [Tooltip("Time Wait For Player Harvest, Minus")]
    public float timeWaitHarvestMinus = 60f;
    [Tooltip("Price of land")]
    public int priceLand = 500;
    [Tooltip("Price of Upgrade Device")]
    public int priceUpgrade = 500;
    [Tooltip("Farm Device Upgrade Percent")]
    public int upgradePercent = 10;
}
