using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class WorkerSystem : MonoBehaviour
{
    public static WorkerSystem Instance;
    public EventHandler OnUpdateVisual;

    private const string DATA_NAME = "Worker";

    [SerializeField] private GlobalInforSO globalInforSO;
    [SerializeField] private NewGameSO newGameSO;

    private int worker;
    private List<Worker> workersList;
    private Queue<Action> jobs;

    private void Awake()
    {
        Instance = this;

        LoadData();

        workersList = new List<Worker>();
        for (int i = 0; i < worker; i++)
        {
            workersList.Add(new Worker());
        }
    }

    private void Start()
    {
        SearchingJobs();
    }

    private void Update()
    {
        if (jobs.Count <= 0)
        {
            SearchingJobs();
        }

        foreach (var worker in workersList)
        {
            if (worker.isIdle())
            {
                if (jobs.Count <= 0) return;
                worker.SetJob(jobs.Dequeue(), globalInforSO.workerTimeFinishActionSecond);
                OnUpdateVisual?.Invoke(this, EventArgs.Empty);
            }
            else worker.Working();
        }
    }

    private void SearchingJobs()
    {
        jobs= new Queue<Action>();

        var placeObjectList = GridSystem.Instance.GetAllPlaceObjectAvailable();

        foreach (var placeObject in placeObjectList)
        {
            if (placeObject.CanHarvest()) jobs.Enqueue(placeObject.Harvest);
            if (!placeObject.IsHasAgricultural()) jobs.Enqueue(() =>
            {
                var seed = Inventory.Instance.GetAllSeeds().Where(x => x.amounts > 0).FirstOrDefault();
                if (seed == null) return;

                AgriculturalSO agriculturalSO = Resources.Load<AgriculturalSO>(seed.name);

                GridSystem.Instance.Planting(placeObject, agriculturalSO);
            });
        }
    }

    public void SetWorker()
    {
        worker++;
        workersList.Add(new Worker());
    }

    public int GetNumberWorkerIdle()
    {
        return workersList.Count((s) => s.isIdle());
    }
    public int GetNumberWorkerWorking()
    {
        return workersList.Count((s) => !s.isIdle());

    }

    #region Save Load
    public class SaveObject
    {
        public int worker;
    }

    public void SaveData()
    {
        SaveObject saveObject = new SaveObject()
        {
            worker = worker,
        };

        SaveSystem.SaveObject(DATA_NAME ,saveObject);
    }

    public void LoadData()
    {
        SaveObject saveObject = SaveSystem.LoadObject<SaveObject>(DATA_NAME);
        
        if (saveObject == null)
        {
            NewGame();
            return;
        }

        worker = saveObject.worker;
    }

    private void NewGame()
    {
        worker = newGameSO.Wolker;
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    #endregion
    [Serializable]
    public class Worker
    {
        private float timeCount;
        private Action job;
        private State state;

        public void SetJob(Action job, float timeCount)
        {
            this.job = job;
            this.timeCount = timeCount;
            state = State.Working;
        }

        public void Working()
        {
            timeCount -= Time.deltaTime;

            if (timeCount < 0) 
            {
                job?.Invoke();
                ResetWorker();
            }
        }

        private void ResetWorker()
        {
            state = State.Idle;
            job = null;
            timeCount = 0;
        }

        public enum State
        {
            Idle,
            Working
        }

        public bool isIdle()
        {
            return state == State.Idle;
        }
    }
}

