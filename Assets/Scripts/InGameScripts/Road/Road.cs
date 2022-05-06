using UnityEngine;
using UnityEngine.SceneManagement;

public class Road : MonoBehaviour
{
    static private Road _lastMovedRoadPiece;
    [SerializeField] private float _roadLengthX3;


    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Game")
        return;

        Instantiate(GameStateManager._instance.SelectedMap.GetModel(), transform);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Road Moved");
            if (_lastMovedRoadPiece != null)
            {
                
                _lastMovedRoadPiece.transform.position += new Vector3(0, _roadLengthX3);
            }
            _lastMovedRoadPiece = this;
        }
    }


}
