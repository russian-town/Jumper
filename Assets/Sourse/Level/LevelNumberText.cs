using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelNumberText : MonoBehaviour
{
    private TMP_Text _text;

    public void Initialize(int levelNumber)
    {
        _text = GetComponent<TMP_Text>();
        _text.text = $"LEVEL {levelNumber}";
    }
}
