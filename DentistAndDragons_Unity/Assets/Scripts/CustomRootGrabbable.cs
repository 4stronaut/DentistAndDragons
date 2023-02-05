using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRootGrabbable : MonoBehaviour
{
    [SerializeField]
    private GameObject _yOffsetGameObject;

    [SerializeField]
    private Rigidbody _leftHandRigidbody;

    private float _accumulatedDeltaRotation = 0f;
    [SerializeField]
    private float _maxWiggleToPull = 2000f;

    [SerializeField]
    private GameObject _leftHandAnchor;

    [SerializeField]
    private GameObject destructionEffect;

    private Vector3 _startPos;
    private Quaternion _currentRot;

    private Vector3 _lastPos;
    private Quaternion _lastRot;

    public bool _isPullable = false;

    public enum RootState
    {
        Released,
        Grabbed,
        Held
    }
    private RootState _state = RootState.Released;

    private void Start()
    {
        if ( !_leftHandAnchor )
            _leftHandAnchor = Globals.instance.getLeftHandAnchor ();

        if ( !_leftHandRigidbody )
            _leftHandRigidbody = Globals.instance.getLeftHandRigidbody ();
        _lastPos = _leftHandAnchor.transform.position;
        _lastRot = _leftHandAnchor.transform.rotation;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isPullable && _state == RootState.Released && other.gameObject.layer == 4 && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            _state = RootState.Grabbed;

            _startPos = Vector3.Normalize(_leftHandAnchor.transform.position - this.transform.position);
            _currentRot = this.transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        // Drop the root
        if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            if (_state == RootState.Held)
            {
                this.transform.SetParent(null);
                Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
                if (_leftHandRigidbody)
                {
                    Vector3 newpos = _leftHandAnchor.transform.position;
                    Vector3 diff = newpos - _lastPos;
                    rb.velocity = diff / Time.deltaTime;
                    rb.angularVelocity = Vector3.one * 2;
                }
                rb.useGravity = true;
                this.GetComponent<Collider>().isTrigger = false;
            }

            _state = RootState.Released;
        }

        // Pull it out
        if (_state == RootState.Grabbed) 
        {
            Vector3 closestPoint = Vector3.Normalize(_leftHandAnchor.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.FromToRotation(_startPos, closestPoint) * _currentRot;

            float deltaAngle = Quaternion.Angle(_lastRot, _leftHandAnchor.transform.rotation);
            _accumulatedDeltaRotation += deltaAngle;
            _lastRot = _leftHandAnchor.transform.rotation;

            if (_yOffsetGameObject)
                _yOffsetGameObject.transform.localPosition = new Vector3(0f, 0.3f * _accumulatedDeltaRotation / _maxWiggleToPull, 0f);

            if (_accumulatedDeltaRotation >= _maxWiggleToPull)
            {
                this.transform.SetParent(_leftHandAnchor.transform);
                _state = RootState.Held;

                DragonVoice.Instance.PlayDragonHurtSound();
                Instantiate(destructionEffect, _yOffsetGameObject.transform.position, Quaternion.LookRotation(-this.transform.up));
            }
            
        }

        _lastPos = _leftHandAnchor.transform.position;
    }

}
