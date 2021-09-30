using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData",menuName = "CreatGameData")]
public class GameData : ScriptableObject
{
    public UiTitle _uiTitle = UiTitle.Start;
    public string Current = "";
}

public enum UiTitle
{
    Start,
    GoodEnd,
    BadEnd
}