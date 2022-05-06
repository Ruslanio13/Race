using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    [SerializeField] private float _accelTime;
    [SerializeField] private float _preSlowingTime;
    [SerializeField] private float _slowingTime;
    [SerializeField] private float _accelKoef;

    private Player _player;
    private float _speedBonus;
    private float _speedBonusBeforePause;

    private void Start()
    {
        _preSlowingTime += GameStateManager._instance.SelectedCar.BonusAccelUpgradeLVL;
        GameStateManager._instance.OnGamePaused += OnPause;
        GameStateManager._instance.OnGameUnpaused += OnUnpause;
        _speedBonus = _player.GetRealPlayerSpeed();
    }

    void Update()
    {
        if(GameStateManager._instance.IsGamePaused)
            return;
        gameObject.transform.position += new Vector3(0, -_speedBonus * Time.deltaTime, 0);
    }

    public IEnumerator Accelerate()
    {
        Timer timer = new Timer(_accelTime);
        PickUpsManager._instance.BonusTimeLeft = _accelTime + _preSlowingTime + _slowingTime;

        while (!timer.TimerExpired)
        {
            if (!GameStateManager._instance.IsGamePaused && !GameStateManager._instance.IsGameOver)
            {
                _player.SetPlayerSpeed(_player.GetRealPlayerSpeed() + Time.deltaTime * _accelKoef);

                PickUpsManager._instance.BonusTimeLeft -= timer.DeltaTimePassed;
                timer.Update(Time.fixedDeltaTime);
            }
            yield return new WaitForFixedUpdate();
        }


        timer.Set(_preSlowingTime);
        while (!timer.TimerExpired)
        {
            if (!GameStateManager._instance.IsGamePaused && !GameStateManager._instance.IsGameOver)
            {

                PickUpsManager._instance.BonusTimeLeft -= timer.DeltaTimePassed;
                timer.Update(Time.fixedDeltaTime);
            }
            yield return new WaitForFixedUpdate();
        }

        timer.Set(_slowingTime);
        while (!timer.TimerExpired)
        {
            if (!GameStateManager._instance.IsGamePaused && !GameStateManager._instance.IsGameOver)
            {
                _player.SetPlayerSpeed(_player.GetRealPlayerSpeed() - Time.deltaTime * _accelKoef);

                PickUpsManager._instance.BonusTimeLeft -= timer.DeltaTimePassed;
                timer.Update(Time.fixedDeltaTime);
            }
            yield return new WaitForFixedUpdate();
        }
    }



    public void SetInformation(Player player)
    {
        _player = player;
    }

    private void OnPause()
    {
        _speedBonusBeforePause = _speedBonus;
        _speedBonus = 0;

    }

    private void OnUnpause() => _speedBonus = _speedBonusBeforePause;


    private void OnDestroy()
    {
        GameStateManager._instance.OnGameOver -= OnPause;
        GameStateManager._instance.OnGameOver -= OnUnpause;
    }
}