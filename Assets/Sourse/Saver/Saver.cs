using UnityEngine;

public class Saver : MonoBehaviour
{
    private const string SelectedIDKey = "SelectedID";
    private const int DefaultPlayerID = 6;
    private const string LanguageKey = "LanguageKey";
    private const string RusLanguage = "Rus";

    public void SaveSelectedID(int id)
    {
        PlayerPrefs.SetInt(SelectedIDKey, id);
        PlayerPrefs.Save();
    }

    public int GetSelectedID()
    {
        if (PlayerPrefs.HasKey(SelectedIDKey))
            return PlayerPrefs.GetInt(SelectedIDKey);
        else
            return DefaultPlayerID;
    }

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

    public void SaveLanguage(Language language)
    {
        PlayerPrefs.SetString(LanguageKey, language.ToString());
        PlayerPrefs.Save();
    }

    public string GetLanguage()
    {
        if (PlayerPrefs.HasKey(LanguageKey))
            return PlayerPrefs.GetString(LanguageKey);
        else
            return RusLanguage;
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

    public void SaveState(string key, int skinID, bool state)
    {
        if (state == true)
            PlayerPrefs.SetInt($"{key}{skinID}", 1);
        else
            PlayerPrefs.SetInt($"{key}{skinID}", 0);
    }

    public bool GetState(string key, int skinID)
    {
        if(PlayerPrefs.HasKey($"{key}{skinID}"))
        {
            return PlayerPrefs.GetInt($"{key}{skinID}") == 1;
        }

        return false;
    }

    public void TryDeleteSaveData(string key)
    {
        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.DeleteKey(key);
    }
}
