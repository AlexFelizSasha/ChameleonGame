using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyChoice : MonoBehaviour
{
    [SerializeField] private DifficultySO _difficultySO;

    public static GameConstantsSO chosenDifficultySO;

    private void Start()
    {
        MainMenuUI.OnEasyStartButtonClicked += MainMenuUIOnEasyStartButtonClicked;
        MainMenuUI.OnMediumStartButtonClicked += MainMenuUIOnMediumStartButtonClicked;
        MainMenuUI.OnHardStartButtonClicked += MainMenuUIOnHardStartButtonClicked;
    }
    private void OnDisable()
    {
        MainMenuUI.OnEasyStartButtonClicked -= MainMenuUIOnEasyStartButtonClicked;
        MainMenuUI.OnMediumStartButtonClicked -= MainMenuUIOnMediumStartButtonClicked;
        MainMenuUI.OnHardStartButtonClicked -= MainMenuUIOnHardStartButtonClicked;
    }

    private void MainMenuUIOnEasyStartButtonClicked()
    {
        chosenDifficultySO = _difficultySO.easy;
        Debug.Log("level easy chosen");
    }

    private void MainMenuUIOnMediumStartButtonClicked()
    {
        chosenDifficultySO = _difficultySO.medium;
        Debug.Log("level medium chosen");
    }

    private void MainMenuUIOnHardStartButtonClicked()
    {
        chosenDifficultySO = _difficultySO.hard;
        Debug.Log("level hard chosen");
    }
}
