using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GlobalInforSO globalInfor;
    void Update()
    {
        UpdateTimeScale();
    }

#if UNITY_EDITOR
    private void UpdateTimeScale()
    {
        if (globalInfor == null) return;

        Time.timeScale = globalInfor.timeScale;
    }
#endif
}
