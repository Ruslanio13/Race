using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager _instance;
    public Collider _playerCollider { get; set; }
    


    private void Awake()
    {
        if (_instance == null)
            _instance = this;

    }

    public void HandleCollision(GameObject CollidedObject)
    {
        switch (CollidedObject.tag)
        {
            case "Car":
                {
                    Debug.Log("GameOver!");
                    GameStateManager._instance.OnGameOver?.Invoke();
                    break;
                }

            case "Coin":
                {
                    PickUpsManager._instance.OnCoinPickUp?.Invoke(CollidedObject.GetComponent<MoveCoins>().GetNominallo());
                    Destroy(CollidedObject);
                    break;
                }
            case "Acceleration":
                {
                    Debug.Log("Accel Picked Up");
                    StartCoroutine(CollidedObject.gameObject.GetComponent<Acceleration>().Accelerate());
                    Destroy(CollidedObject);
                    PickUpsManager._instance.OnAccelerationPickUp?.Invoke(PickUpsManager._instance.BonusTimeLeft);
                    break;
                }
            case "Border":
                {
                    GameStateManager._instance.DrivingOnOncoming = (_playerCollider.gameObject.transform.position.x < CollidedObject.transform.position.x);
                    break;
                }
        }
    }
}
