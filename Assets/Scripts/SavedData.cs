using System;
using UnityEngine;

public class SavedData: MonoBehaviour
{
    public enum Data
    {
        Volume,
        PlayerColor,
        PlayerFace
    }
    
    public static SavedData Instance {  get; private set; }


    public event Action OnDataChanged;

    private void Awake()
    {
        Instance = this;
    }

    public string GetString(Data data)
    {
        return PlayerPrefs.GetString(data.ToString());
    }

    public string GetString(Data data, string defaultString)
    {
        return PlayerPrefs.GetString(data.ToString(), defaultString);
    }

    public void SetString(Data data, string value)
    {
        PlayerPrefs.SetString(data.ToString(), value);

        OnDataChanged?.Invoke();
    }

    public float GetFloat(Data data)
    {
        return PlayerPrefs.GetFloat(data.ToString());
    }

    public float GetFloat(Data data, float defaultFloat)
    {
        return PlayerPrefs.GetFloat(data.ToString(), defaultFloat);
    }

    public void SetFloat(Data data, float value)
    {
        PlayerPrefs.SetFloat(data.ToString(), value);

        OnDataChanged?.Invoke();
    }

    public bool HasKey(Data data)
    {
        return PlayerPrefs.HasKey(data.ToString());
    }
}
