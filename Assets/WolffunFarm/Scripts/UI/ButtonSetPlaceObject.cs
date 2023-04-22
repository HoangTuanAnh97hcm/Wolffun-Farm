using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSetPlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject placeObject;
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SetPlaceObject);
    }

    private void SetPlaceObject()
    {

    }
}
