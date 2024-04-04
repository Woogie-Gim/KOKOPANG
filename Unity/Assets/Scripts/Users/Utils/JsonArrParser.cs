using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonArrParser
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static T[] FromJson<T>(string json)
    {
        json = makeJsonString(json);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return UnityEngine.JsonUtility.ToJson(wrapper);
    }

    private static string makeJsonString(string json)
    {
        return "{\"Items\":" + json + "}";
    }
}
