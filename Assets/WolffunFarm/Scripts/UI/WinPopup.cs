using UnityEngine;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private RectTransform youWin_txt;

    private void OnEnable()
    {
        youWin_txt.localScale = Vector3.zero;
    }

    private float timeCount = 0;
    private float timeAnimation = 2;
    private void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < 2)
        {
            youWin_txt.localScale = new Vector2(timeCount, timeCount);
        }
        else Time.timeScale = 0;
    }
}
