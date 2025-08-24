using System;
using UnityEngine;

public static class SavedData
{
    public enum Data
    {
        Volume,
        PlayerColor,
        PlayerFace
    }

    public static event Action OnDataChanged;

    public static string GetString(Data data)
    {
        return PlayerPrefs.GetString(data.ToString());
    }

    public static string GetString(Data data, string defaultString)
    {
        return PlayerPrefs.GetString(data.ToString(), defaultString);
    }

    public static void SetString(Data data, string value)
    {
        PlayerPrefs.SetString(data.ToString(), value);

        OnDataChanged?.Invoke();
    }

    public static float GetFloat(Data data)
    {
        return PlayerPrefs.GetFloat(data.ToString());
    }

    public static float GetFloat(Data data, float defaultFloat)
    {
        return PlayerPrefs.GetFloat(data.ToString(), defaultFloat);
    }

    public static void SetFloat(Data data, float value)
    {
        PlayerPrefs.SetFloat(data.ToString(), value);

        OnDataChanged?.Invoke();
    }

    public static bool HasKey(Data data)
    {
        return PlayerPrefs.HasKey(data.ToString());
    }
}
