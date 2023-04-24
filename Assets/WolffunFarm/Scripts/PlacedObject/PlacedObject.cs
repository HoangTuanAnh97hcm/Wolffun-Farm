using System;
using Unity.Android.Gradle;
using UnityEngine;
public class PlacedObject : MonoBehaviour {

    public static PlacedObject Create(Vector3 worldPosition, Transform placedObjectPrefab) 
    {
        Transform placedObjectTransform = Instantiate(placedObjectPrefab, worldPosition, Quaternion.identity);
        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        return placedObject;
    }

    public enum State
    {
        Idle,
        Produce,
        WaitHarvest
    }

    [SerializeField] GlobalInforSO globalInfor;

    private State state;
    private int product;
    private int produced;
    private float timeCount;
    private AgriculturalSO agriculturalSO;
    private AgriculturalVisual visual;

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Produce:
                if (produced < agriculturalSO.totalProduct)
                {
                    CountTime(globalInfor.upgradePercent);

                    if (timeCount < 0)
                    {
                        product++;
                        produced++;
                        SetTimeProduce();
                    }

                    SetAgriculturalVisual(product, timeCount);

                }
                else
                {
                    StartWailtHarvest();
                }
                break;
            case State.WaitHarvest:

                CountTime(globalInfor.upgradePercent);
                SetAgriculturalVisual(product, timeCount, Color.red);

                if (timeCount < 0)
                {
                    ResetObject();
                }
                break;
        }
    }

    #region Time
    private void SetTimeProduce()
    {
        timeCount = UtilsClass.MinusToSecond(agriculturalSO.productTimeMinus);
    }

    private void CountTime()
    {
        timeCount -= Time.deltaTime;
    }

    private void CountTime(float percent)
    {
        timeCount -= Time.deltaTime + (Time.deltaTime * percent / 100);
    }
    #endregion

    #region Agricultura Visual
    private void SetVisual(int product, TimeSpan timeSpan)
    {
        visual?.SetText(agriculturalSO.name, product, timeSpan);
    }

    private void SetAgriculturalVisual(int product, float second)
    {
        visual?.SetText(agriculturalSO.name, product, second);
    }

    private void SetAgriculturalVisual(int product, float second, Color colorSecond)
    {
        visual?.SetText(agriculturalSO.name, product, second, colorSecond);
    }

    private void CreateAgriculturaVisual()
    {
        visual = Instantiate(agriculturalSO.visualPrefab, transform);
    }

    private bool HasPlanted()
    {
        return visual == null;
    }
    #endregion

    private void ResetObject()
    {
        agriculturalSO = null;
        product = 0;
        produced = 0;
        visual?.DestroySelf();
        visual = null;
        timeCount = 0;
        state = State.Idle;
    }

    public void SetAgricultural(AgriculturalSO agriculturalSO)
    {
        if (IsHasAgricultural()) return;

        this.agriculturalSO = agriculturalSO;
    }

    public bool IsHasAgricultural()
    {
        return agriculturalSO != null;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public void StartStateProduction()
    {
        if (!HasPlanted()) return;

        CreateAgriculturaVisual();

        SetTimeProduce();

        state = State.Produce;
    }

    private void StartWailtHarvest()
    {
        timeCount = UtilsClass.MinusToSecond(globalInfor.timeWaitHarvestMinus);
        state = State.WaitHarvest;
    }

    public void Harvest()
    {
        if (!IsHasAgricultural() || product == 0) return;

        Inventory.Instance.SetAmountProduct(agriculturalSO.name, product);
        product = 0;

        if (produced == agriculturalSO.totalProduct) ResetObject();
    }

    #region Save Load
    [System.Serializable]
    public class SaveObject
    {
        public string agriculturalName;
        public int product;
        public int produced;
        public DateTime dateTime;
        public State state;
    }

    public string SaveData()
    {
        SaveObject saveObject = new SaveObject()
        {
            agriculturalName = agriculturalSO?.name,
            product = product,
            produced = produced,
            dateTime = DateTime.Now + TimeSpan.FromSeconds(timeCount),
            state = state
        };

        return SaveSystem.NewtonsoftSerializeObject(saveObject);
    }

    public void LoadData(string data)
    {
        SaveObject saveObject = SaveSystem.NewtonsoftDeserializeObject<SaveObject>(data);
        AgriculturalSO agriculturalSO = Resources.Load<AgriculturalSO>(saveObject.agriculturalName);

        if (agriculturalSO == null) return;

        SetAgricultural(agriculturalSO);
        CreateAgriculturaVisual();
        state = saveObject.state;

        TimeSpan timeSpan = DateTime.Now - saveObject.dateTime;
        int productBonus = 0;

        if (timeSpan.TotalSeconds < 0)
        {
            timeCount = Mathf.Abs((float)timeSpan.TotalSeconds);
        }
        else
        {
            productBonus = (int)((float)timeSpan.TotalMinutes / agriculturalSO.productTimeMinus);
        }

        if (state == State.WaitHarvest)
        {
            produced = 0;
            timeCount = (float)(saveObject.dateTime - DateTime.Now).TotalSeconds;
        }

        int productMax = agriculturalSO.totalProduct - (saveObject.produced - saveObject.product);

        product = Math.Clamp(saveObject.product + productBonus, 0, productMax);
        produced = Math.Clamp(saveObject.produced + productBonus, 0, agriculturalSO.totalProduct);
    }
    #endregion
}
