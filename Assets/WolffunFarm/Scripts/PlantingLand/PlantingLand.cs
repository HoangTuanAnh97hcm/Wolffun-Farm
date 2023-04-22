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

    private State state;
    private int product;
    private int produced;

    private DateTime dateTime;
    private int timeWaitHarvest = 1;

    private AgriculturalVisual visual;

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Produce:
                if (produced < GetAgriculturalSO().totalProduct)
                {
                    TimeSpan timeSpan = dateTime - DateTime.Now;

                    if (timeSpan.TotalMinutes < 0)
                    {
                        product++;
                        produced++;
                        SetTimeProduce();
                    }

                    SetVisual(product, timeSpan);

                }else
                {
                    dateTime = DateTime.Now + TimeSpan.FromHours(timeWaitHarvest);
                    state = State.WaitHarvest;
                }
                break;
            case State.WaitHarvest:
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
    }

    private void SetVisual(int product, TimeSpan timeSpan)
    {
        visual?.SetText(GetAgriculturalSO().name, product, timeSpan);
    }

    private void CreateVisual(Transform transform)
    {
        visual = Instantiate(GetAgriculturalSO().visualPrefab, transform);
    }
    
    private bool CanCreateVisual()
    {
        return visual == null;
    }
}
