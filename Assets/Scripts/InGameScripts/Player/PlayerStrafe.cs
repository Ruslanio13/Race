using UnityEngine;

public class PlayerStrafe : MonoBehaviour
{
    [SerializeField] private GameObject _leftBorder;
    [SerializeField] private GameObject _rightBorder;
    [SerializeField] private BoxCollider _leftBorderCollider;
    [SerializeField] private BoxCollider _rightBorderCollider;
    [SerializeField] private RotationAnim _rotAnim;
    [SerializeField] private Player _player;
    private float _leftBorderX;
    private float _rightBorderX;


    private enum StuckState
    {
        STUCK_TO_THE_LEFT = -1,
        UNSTUCKED = 0,
        STUCK_TO_THE_RIGHT = 1,
    }
    private StuckState _stuckState;

    private float _strafeSpeed;
    private float _speed;
    private float _direction;

    private void Start()
    {
        _direction = 0;
        _leftBorderX = _leftBorder.transform.position.x + 0.8f * _leftBorderCollider.size.x;
        _rightBorderX = _rightBorder.transform.position.x - 0.8f *_rightBorderCollider.size.x;

        Debug.Log("Left border x is: " + _leftBorderX);
    }

    private void FixedUpdate()
    {
        MoveToTheDirection();
    }


    private void MoveToTheDirection()
    {
        if (_player.GetPlayerStrafeSpeed() == 0)
        {
            _rotAnim.SetRotationDir(RotationAnim.RotateDir.RETURNING);
            return;
        }


        if (gameObject.transform.position.x >= _leftBorderX && gameObject.transform.position.x <= _rightBorderX)
            transform.position += new Vector3(_direction * _player.GetPlayerStrafeSpeed() * Time.deltaTime, 0);
        else
            StickToTheWall(_leftBorderX, _rightBorderX);
    }

    private void StickToTheWall(float border1, float border2)
    {
        float dist1 = Mathf.Abs(transform.position.x - border1);
        float dist2 = Mathf.Abs(transform.position.x - border2);

        float requiredBorder;

        if (dist1 < dist2)
        {
            requiredBorder = border1;
            _stuckState = StuckState.STUCK_TO_THE_LEFT;
        }
        else
        {
            requiredBorder = border2;
            _stuckState = StuckState.STUCK_TO_THE_RIGHT;
        }
        _rotAnim.SetRotationDir(RotationAnim.RotateDir.RETURNING);
        transform.position = new Vector3(requiredBorder, transform.position.y);
        _direction = 0;
    }

    public void ChangeDirection(Vector2 dir)
    {
        if ((int)_stuckState != dir.x)
        {
            _stuckState = StuckState.UNSTUCKED;
            _direction += dir.x;
            _rotAnim.SetRotationDir((RotationAnim.RotateDir)dir.x);
        }

    }
    public void ResetDirection()
    {
        _direction = 0;
        _rotAnim.SetRotationDir((RotationAnim.RotateDir)0);
    }
}