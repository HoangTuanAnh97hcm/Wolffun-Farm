using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost2D : MonoBehaviour {

    private Transform visual;
    private AgriculturalSO placedObjectTypeSO;

    private void Start() {
        RefreshVisual();

        GridBuildingSystem2D.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) {
        RefreshVisual();
    }

    private void LateUpdate() {
        Vector3 targetPosition = GridBuildingSystem2D.Instance.GetMouseWorldSnappedPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }

        AgriculturalSO placedObjectTypeSO = GridBuildingSystem2D.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            visual = Instantiate(placedObjectTypeSO.visualPrefab.transform, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
        }
    }

}
