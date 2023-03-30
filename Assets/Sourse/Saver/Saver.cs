using UnityEngine;

public class Saver : MonoBehaviour
{
    public void Save(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void Save(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public bool TryGetValue(string key, out float value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            value = PlayerPrefs.GetFloat(key);
            return true;
        }
        else
        {
            value = 0;
            return false;
        }
    }

    public bool TryGetValue(string key, out int value)
    {
        if (PlayerPrefs.HasKey(key))
        {
            value = PlayerPrefs.GetInt(key);
            return true;
        }
        else
        {
            value = 0;
            return false;
        }
    }

    public bool TryGetValue(string key)
    {
        if (PlayerPrefs.HasKey(key))
            if (PlayerPrefs.GetInt(key) == 1)
                return true;
            else
                return false;

        return false;
    }

    public bool TryGetValue(int key)
    {
        if (PlayerPrefs.HasKey(key.ToString()))
            if (PlayerPrefs.GetInt(key.ToString()) == 1)
                return true;
            else
                return false;

        return false;
    }

    public void TryDeleteSaveData(string key)
    {
        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.DeleteKey(key);
    }
}
