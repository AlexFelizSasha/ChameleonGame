using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringMachineAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _particles;
    [SerializeField] private WateringMachine _wateringMachine;

    private void Awake()
    {
        StopParticles();
    }
    private void Start()
    {
        _wateringMachine.OnStartWatering += _wateringMachine_OnStartWatering;
        _wateringMachine.OnStopWatering += _wateringMachine_OnStopWatering;
    }

    private void _wateringMachine_OnStopWatering(object sender, System.EventArgs e)
    {
        StopParticles();
    }

    private void _wateringMachine_OnStartWatering(object sender, System.EventArgs e)
    {
        StartParticles();
    }

    private void StartParticles()
    {
        _particles.SetActive(true);
    }
    private void StopParticles()
    {
        _particles.SetActive(false);
    }
}
