using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField] private SoundSO _soundSO;
    [SerializeField] private WateringMachine _wateringMachine;
    [SerializeField] private AudioSource _waterAudio;
    [SerializeField] private Player _player;
    [SerializeField] private Garden _garden;
    [SerializeField] private CollectButtonUI _collectButton;
    [SerializeField] private Transform _cameraTransform;

    private float _volume = 0.5f;
    private Vector3 _cameraPosition;
    private void Start()
    {
        _cameraPosition = _cameraTransform.position;
        PauseButton.OnPauseButtonClicked += OnPauseButtonClicked;
        PauseMenuUI.OnPausePlayButtonClicked += PauseMenuUI_OnPausePlayButtonClicked;
        PauseMenuUI.OnPauseQuitButtonClicked += PauseMenuUI_OnPauseQuitButtonClicked;
        PauseMenuUI.OnPauseMenuButtonClicked += PauseMenuUI_OnPauseMenuButtonClicked;

        PlayerLifeManager.OnLifeManagerGameOver += PlayerLifeManager_OnLifeManagerGameOver;
        Garden.OnTreesDead += Garden_OnTreesDead;

        GameOverMenuUI.OnGameOverQuitButtonClicked += GameOverMenuUI_OnGameOverQuitButtonClicked;
        GameOverMenuUI.OnGameOverRestartButtonClicked += GameOverMenuUI_OnGameOverRestartButtonClicked;
        GameOverMenuUI.OnGameOverMenuButtonClicked += GameOverMenuUI_OnGameOverMenuButtonClicked;

        Loot.OnLootScoreAdd += LootOnLootScoreAdd;
        GeyserCollider.OnDropForBlock += GeyserCollider_OnDropForBlock;

        Block.OnKillPlayer += Block_OnKillPlayer;

        _wateringMachine.OnStartWatering += WateringMachine_OnStartWatering;
        _wateringMachine.OnStopWatering += WateringMachine_OnStopWatering;
        _player.OnPlayerMoves += Player_OnPlayerMoves;
        _collectButton.OnCollectButtonClicked += CollectButton_OnCollectButtonClicked;
    }


    private void OnDisable()
    {
        PauseButton.OnPauseButtonClicked -= OnPauseButtonClicked;
        PauseMenuUI.OnPausePlayButtonClicked -= PauseMenuUI_OnPausePlayButtonClicked;
        PauseMenuUI.OnPauseQuitButtonClicked -= PauseMenuUI_OnPauseQuitButtonClicked;
        PauseMenuUI.OnPauseMenuButtonClicked -= PauseMenuUI_OnPauseMenuButtonClicked;

        PlayerLifeManager.OnLifeManagerGameOver -= PlayerLifeManager_OnLifeManagerGameOver;
        Garden.OnTreesDead -= Garden_OnTreesDead;

        GameOverMenuUI.OnGameOverQuitButtonClicked -= GameOverMenuUI_OnGameOverQuitButtonClicked;
        GameOverMenuUI.OnGameOverRestartButtonClicked -= GameOverMenuUI_OnGameOverRestartButtonClicked;
        GameOverMenuUI.OnGameOverMenuButtonClicked -= GameOverMenuUI_OnGameOverMenuButtonClicked;

        Loot.OnLootScoreAdd -= LootOnLootScoreAdd;
        GeyserCollider.OnDropForBlock -= GeyserCollider_OnDropForBlock;

        Block.OnKillPlayer -= Block_OnKillPlayer;
    }

    private void Block_OnKillPlayer(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.playerDead, _cameraPosition, _volume);
    }
    private void CollectButton_OnCollectButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.froot, _cameraPosition, _volume);
    }
    private void Player_OnPlayerMoves()
    {
        AudioSource.PlayClipAtPoint(_soundSO.playerMoves, _cameraPosition, _volume);
    }


    private void GeyserCollider_OnDropForBlock(object sender, GeyserCollider.OnDropForBlockEventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.dropInGeyser, _cameraPosition, _volume);
    }
    private void LootOnLootScoreAdd(object sender, Loot.OnLootScoreAddEventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.dropInBaggage, _cameraPosition, _volume);
    }
    private void WateringMachine_OnStopWatering(object sender, System.EventArgs e)
    {
        _waterAudio.Stop();
    }
    private void WateringMachine_OnStartWatering(object sender, System.EventArgs e)
    {
        _waterAudio.Play();
    }
    private void GameOverMenuUI_OnGameOverMenuButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }

    private void GameOverMenuUI_OnGameOverRestartButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }

    private void GameOverMenuUI_OnGameOverQuitButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }

    private void Garden_OnTreesDead()
    {
        AudioSource.PlayClipAtPoint(_soundSO.gameOver, _cameraPosition, _volume);
    }

    private void PlayerLifeManager_OnLifeManagerGameOver(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.gameOver, _cameraPosition, _volume);
    }

    private void PauseMenuUI_OnPauseMenuButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }

    private void PauseMenuUI_OnPauseQuitButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }


    private void PauseMenuUI_OnPausePlayButtonClicked(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }


    private void OnPauseButtonClicked()
    {
        AudioSource.PlayClipAtPoint(_soundSO.button, _cameraPosition, _volume);
    }
}
