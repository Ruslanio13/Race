using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private GameObject _followedTarget;
    public float OffsetX{get;set;} = 0;
    public float OffsetY{get;set;} = 0;
    public void FollowPlayer()
    {
        transform.position = new Vector3(OffsetX, _followedTarget.transform.position.y + OffsetY, transform.position.z);
    }
    public void SetOffsets()
    {
        OffsetX = transform.position.x;
        OffsetY = transform.position.y;
    }

    private void Start()
    {
        SetOffsets();
    }

    private void Update()
    {
        FollowPlayer();
    }
}
