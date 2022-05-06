using UnityEngine;

class UpperOrLowerBorder : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Coin":
                {
                    Destroy(col.gameObject);
                }
                break;
        }
    }
}