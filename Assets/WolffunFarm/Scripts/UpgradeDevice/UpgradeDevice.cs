using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeDevice : MonoBehaviour
{
    [SerializeField] GlobalInforSO globalInforSO;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetLevelDevice);
    }

    private void SetLevelDevice()
    {
        if (GameData.Instance.GetCoint() < globalInforSO.priceUpgrade) return;

        GameData.Instance.SetLevelDevice();
        GameData.Instance.SetCoint(-globalInforSO.priceUpgrade);
    }
}
