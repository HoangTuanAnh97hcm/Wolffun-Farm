using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HideWorker : MonoBehaviour
{
    [SerializeField] private GlobalInforSO globalInforSO;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Hide);
    }

    public void Hide()
    {
        if (GameData.Instance.GetCoint() < globalInforSO.priceWorker) return;

        GameData.Instance.SetCoint(-globalInforSO.priceWorker);
        WorkerSystem.Instance.SetWorker();
    }
}
