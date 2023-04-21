using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedObject : MonoBehaviour {

    public static PlacedObject Create(Vector3 worldPosition, Transform placedObjectPrefab) 
    {
        Transform placedObjectTransform = Instantiate(placedObjectPrefab, worldPosition, Quaternion.identity);
        return placedObjectTransform.GetComponent<PlacedObject>();
    }

    private AgriculturalSO agriculturalSO;

    public void SetAgricultural(AgriculturalSO agriculturalSO)
    {
        this.agriculturalSO = agriculturalSO;
    }

    public AgriculturalSO GetAgriculturalSO()
    {
        return agriculturalSO;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public override string ToString() {
        return agriculturalSO.nameString;
    }
}
