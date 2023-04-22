using UnityEngine;

public abstract class PlacedObject : MonoBehaviour {

    public static PlacedObject Create(Vector3 worldPosition, Transform placedObjectPrefab) 
    {
        Transform placedObjectTransform = Instantiate(placedObjectPrefab, worldPosition, Quaternion.identity);
        return placedObjectTransform.GetComponent<PlacedObject>();
    }

    private AgriculturalSO agriculturalSO;

    public virtual void SetAgricultural(AgriculturalSO agriculturalSO)
    {
        if (IsHaveAgricultural()) return;

        this.agriculturalSO = agriculturalSO;
    }

    public AgriculturalSO GetAgriculturalSO()
    {
        return agriculturalSO;
    }

    public void ClearAgricultural()
    {
        agriculturalSO = null;
    }

    public bool IsHaveAgricultural()
    {
        return agriculturalSO != null;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public override string ToString() {
        return agriculturalSO.nameString;
    }
}
