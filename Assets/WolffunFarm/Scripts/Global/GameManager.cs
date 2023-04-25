using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GlobalInforSO globalInfor;
    [SerializeField] private GameObject winPopup;
    void Update()
    {
        UpdateTimeScale();
    }

    private void Start()
    {
        GameData.OnDataChange += CheckWin;
    }

    public void CheckWin(object seed, GameData.GameDataEventArgs e)
    {
        if (e.coint >= globalInfor.targetCoints)
        {
            Instantiate(winPopup);
        }
    }

#if UNITY_EDITOR
    private void UpdateTimeScale()
    {
        if (globalInfor == null) return;

        Time.timeScale = globalInfor.timeScale;
    }
#endif
}
