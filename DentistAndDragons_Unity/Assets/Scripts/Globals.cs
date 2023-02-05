using UnityEngine;

public class Globals : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftHandAnchor;

    [SerializeField]
    private Rigidbody _leftHandRigidbody;

    public static Globals instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
        }
    }

    public GameObject getLeftHandAnchor()
    {
        return _leftHandAnchor;
    }

    public Rigidbody getLeftHandRigidbody()
    {
        return _leftHandRigidbody;
    }

}