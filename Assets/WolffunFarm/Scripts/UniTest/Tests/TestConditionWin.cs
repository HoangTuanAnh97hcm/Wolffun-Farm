using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static GameData;

public class TestConditionWin
{
    private GlobalInforSO globalInforSO;
    [SetUp]
    public void SetUp()
    {
        globalInforSO = new GlobalInforSO();
    }

    [Test]
    public void TestPositiveValues()
    {
        Assert.AreEqual(true, 999 > globalInforSO.targetCoints);
    }

    [Test]
    public void TestNegativeValues()
    {
        Assert.AreEqual(true, -999 > globalInforSO.targetCoints);
    }

    [Test]
    public void TestZeroValues()
    {
        Assert.AreEqual(true, 0 > globalInforSO.targetCoints);
    }

    [Test]
    public void TestLaggerValues()
    {
        Assert.AreEqual(true, 999999999 > globalInforSO.targetCoints);
    }
}
