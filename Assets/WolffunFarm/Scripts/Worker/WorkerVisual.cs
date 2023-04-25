using System;
using TMPro;
using UnityEngine;

public class WorkerVisual : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateWorkerVisual();
        WorkerSystem.Instance.OnUpdateVisual += UpdateWorkerVisual;
    }

    private void UpdateWorkerVisual(object seed, EventArgs e)
    {
        text.text = $"Worker Idle: {WorkerSystem.Instance.GetNumberWorkerIdle()} - Worker Working: {WorkerSystem.Instance.GetNumberWorkerWorking()}";
    }
    private void UpdateWorkerVisual()
    {
        text.text = $"Worker Idle: {WorkerSystem.Instance.GetNumberWorkerIdle()} - Worker Working: {WorkerSystem.Instance.GetNumberWorkerWorking()}";
    }
}
