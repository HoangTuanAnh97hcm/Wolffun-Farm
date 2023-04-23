using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingLand : PlacedObject
{
    private enum State
    {
        Idle,
        Produce,
        WaitHarvest
    }

    [SerializeField] GlobalInforSO globalInfor;

    private State state;
    private int product;
    private int produced;

    private DateTime dateTime;
    private TimeSpan timeSpan;

    private AgriculturalVisual visual;

    private float timeCount;

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Produce:
                if (produced < GetAgriculturalSO().totalProduct)
                {
                    CountTime(globalInfor.upgradePercent);

                    if (timeCount < 0)
                    {
                        product++;
                        produced++;
                        SetTimeProduce();
                    }

                    SetVisual(product, timeCount);

                }else
                {
                    timeCount = UtilsClass.MinusToSecond(globalInfor.timeWaitHarvestMinus);
                    state = State.WaitHarvest;
                }
                break;
            case State.WaitHarvest:

                CountTime(globalInfor.upgradePercent);
                SetVisual(product, timeCount, Color.red);

                if (timeCount < 0)
                {
                    ResetObject();
                }
                break;
        }
    }

    public override void SetAgricultural(AgriculturalSO agriculturalSO)
    {
        base.SetAgricultural(agriculturalSO);

        if (!CanCreateVisual()) return;

        SetTimeProduce();

        CreateVisual(transform);

        state = State.Produce;
    }

    private void SetTimeProduce()
    {
        dateTime = DateTime.Now + TimeSpan.FromMinutes(GetAgriculturalSO().productTimeMinus);

        timeCount = UtilsClass.MinusToSecond(GetAgriculturalSO().productTimeMinus);
    }

    private void CountTime()
    {
        timeCount -= Time.deltaTime;
    }

    private void CountTime(float percent)
    {
        timeCount -= Time.deltaTime + (Time.deltaTime * percent / 100);
    }

    private void SetVisual(int product, TimeSpan timeSpan)
    {
        visual?.SetText(GetAgriculturalSO().name, product, timeSpan);
    }

    private void SetVisual(int product, float second)
    {
        visual?.SetText(GetAgriculturalSO().name, product, second);
    }

    private void SetVisual(int product, float second, Color colorSecond)
    {
        visual?.SetText(GetAgriculturalSO().name, product, second, colorSecond);
    }

    private void CreateVisual(Transform transform)
    {
        visual = Instantiate(GetAgriculturalSO().visualPrefab, transform);
    }
    
    private bool CanCreateVisual()
    {
        return visual == null;
    }

    public override void ResetObject()
    {
        base.ResetObject();
        product = 0;
        produced = 0;
        visual?.DestroySelf();
        visual = null;
        timeCount = 0;
        state = State.Idle;
    }
}
