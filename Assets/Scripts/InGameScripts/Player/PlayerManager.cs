using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private ControlButton _leftButton;
    [SerializeField] private ControlButton _rightButton;
    [SerializeField] private PlayerStrafe _player;

    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
    }

    private void Start()
    {
        _leftButton.OnPointerClickHandler += () => { _player.ChangeDirection(Vector2.left); };
        _leftButton.OnPointerUpHandler += () => { _player.ResetDirection(); };

        _rightButton.OnPointerClickHandler += () => { _player.ChangeDirection(Vector2.right); };
        _rightButton.OnPointerUpHandler += () => { _player.ResetDirection(); };
    }
}
