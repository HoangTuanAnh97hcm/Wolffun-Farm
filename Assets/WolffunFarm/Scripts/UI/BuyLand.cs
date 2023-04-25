using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuyLand : MonoBehaviour
{
    [SerializeField] private PlacedObject placedObject;
    [SerializeField] private GlobalInforSO globalInforSO;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnBuyLandClick);
    }

    private void OnBuyLandClick()
    {
        if (GameData.Instance.GetCoint() < globalInforSO.priceLand)
        {
            Logging.LogError("You don't have enought Coint");
            return;
        }

        GridSystem.Instance.SetPlancedObject(placedObject);
    }

    private void OnValidate()
    {
        if (placedObject == null)
            Logging.LogWarning("Placed Object is null, you must to give it prefab reference");
    }
}
