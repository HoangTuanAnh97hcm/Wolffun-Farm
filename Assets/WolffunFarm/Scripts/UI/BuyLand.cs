using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuyLand : MonoBehaviour
{
    [SerializeField] private PlacedObject placedObject;
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
        GridSystem.Instance.SetPlaceObject(placedObject);
    }

    private void OnValidate()
    {
        if (placedObject == null)
            Logging.LogWarning("Placed Object is null, you must to give it prefab reference");
    }
}
