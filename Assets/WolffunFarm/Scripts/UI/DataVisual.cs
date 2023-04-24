using System;
using TMPro;
using UnityEngine;

public class DataVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coint;
    [SerializeField] private TextMeshProUGUI levelDevice;

    private void Start()
    {
        GameData.OnDataChange += updateCoint;
        UpdateCoint();
    }

    private void updateCoint(object sender, GameData.GameDataEventArgs e)
    {
        coint.text = $"Coints: {e.coint}";
        levelDevice.text = $"Level Device: {e.levelDevices}";
    }

    private void UpdateCoint()
    {
        coint.text = $"Coints: {GameData.Instance.GetCoint()}";
        levelDevice.text = $"Level Device: {GameData.Instance.GetLevelDevice()}";
    }
}
