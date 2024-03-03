using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    [SerializeField] private SoundSO _soundSO;
    void Start()
    {
        MainMenuUI.OnQuitButtonClicked += SoundOnQuitButtonClicked;
        MainMenuUI.OnEasyStartButtonClicked += SoundOnStartButtonClicked;
    }
    private void OnDisable()
    {
        MainMenuUI.OnQuitButtonClicked -= SoundOnQuitButtonClicked;
        MainMenuUI.OnEasyStartButtonClicked -= SoundOnStartButtonClicked;
    }

    private void SoundOnStartButtonClicked()
    {
        PlayButtonSound();
    }

    private void SoundOnQuitButtonClicked()
    {
        PlayButtonSound();
    }

    private void PlayButtonSound()
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, Vector3.zero, 5f);
    }
}
