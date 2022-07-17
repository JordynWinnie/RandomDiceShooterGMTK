using System.Collections.Generic;
using UnityEngine;

public enum ModificationType
{
    Add,
    Subtract,
    Set,
    Divide,
    Multiply
}

public static class MathHelper
{
    public static void ModifyValue(ModificationType modificationType, ref float value, float amount)
    {
        switch (modificationType)
        {
            case ModificationType.Add:
                value += amount;
                break;
            case ModificationType.Subtract:
                value -= amount;
                break;
            case ModificationType.Set:
                value = amount;
                break;
            case ModificationType.Divide:
                value /= amount;
                break;
            case ModificationType.Multiply:
                value *= amount;
                break;
        }
    }

    public static float RandomBetweenFloats(float a = 0f, float b = 100f)
    {
        return Random.Range(a, b);
    }

    public static int RandomBetweenInts(int a = 0, int b = 100)
    {
        return Random.Range(a, b);
    }

    public static float RandomFromFloatZeroTo(float value)
    {
        return Random.Range(0f, value);
    }

    public static int RandomFromIntZeroTo(int value)
    {
        return Random.Range(0, value);
    }

    public static T RandomFromArray<T>(T[] list)
    {
        return list[RandomFromIntZeroTo(list.Length)];
    }

    public static T RandomFromArray<T>(T[] list, out int index)
    {
        index = RandomFromIntZeroTo(list.Length);
        return list[index];
    }

    public static T RandomFromList<T>(List<T> list)
    {
        return list[RandomFromIntZeroTo(list.Count)];
    }

    public static T RandomFromList<T>(List<T> list, out int index)
    {
        index = RandomFromIntZeroTo(list.Count);
        return list[index];
    }

    public static Vector3 InArea(Vector3 spawnArea)
    {
        var xPos = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
        var yPos = Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f);
        var zPos = Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f);
        return new Vector3(xPos, yPos, zPos);
    }

    public static Vector3 RandomVectorDirectionAroundY()
    {
        var index = RandomFromIntZeroTo(4);
        if (index == 0)
            return Vector3.forward;
        if (index == 1)
            return Vector3.back;
        if (index == 3)
            return Vector3.left;
        return Vector3.right;
    }

    public static Vector3 PointAtCircumferenceXZ(Vector3 center, float radius)
    {
        var theta = RandomFromFloatZeroTo(360);
        var opposite = radius * Mathf.Sin(theta);
        var adjacent = radius * Mathf.Cos(theta);
        return center + new Vector3(adjacent, 0f, opposite);
    }

    public static Vector3 OfVectorDirectionAny()
    {
        var index = RandomFromIntZeroTo(6);
        if (index == 0)
            return Vector3.forward;
        if (index == 1)
            return Vector3.back;
        if (index == 3)
            return Vector3.left;
        if (index == 4)
            return Vector3.right;
        if (index == 5)
            return Vector3.up;
        return Vector3.down;
    }

    //My glorious tier chance, number line, random index generator
    public static int RandomIndex<T>(T[] list, float[] chances)
    {
        var tierChances = new float[list.Length];
        var prevChance = 0f;
        //makes tierChances look like a number line
        //0--[chance 1]--30--[chance 2]--70--[chance 3]--100
        for (var i = 0; i < list.Length; i++)
        {
            tierChances[i] = prevChance + chances[i];
            prevChance = tierChances[i];
        }

        //simple randomizes a number and then check the ranges
        var randomTier = Random.Range(0, 100);
        for (var i = 0; i < tierChances.Length; i++)
        {
            var highNum = i == tierChances.Length - 1 ? 100 : tierChances[i];
            var lowNum = i == 0 ? 0 : tierChances[i - 1];
            if (randomTier > lowNum && randomTier < highNum) return i;
        }

        return 0;
    }
}