using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void FixedUpdate()
    {
        if(GameStateManager._instance.IsGamePaused)
            return;

        transform.Translate(Vector3.up * _player.GetRealPlayerSpeed() * Time.fixedDeltaTime);
    }

}

