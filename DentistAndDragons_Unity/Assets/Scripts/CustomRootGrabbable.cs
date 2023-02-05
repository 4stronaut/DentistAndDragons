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

    private Rigidbody _rigidbody;

    private Transform _oldParent;

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

    private void OnTriggerExit(Collider other)
    {
        if (_isPullable && _state == RootState.Grabbed && other.gameObject.layer == 4 && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            _state = RootState.Released;
        }
    }

    private void FixedUpdate()
    {
        // Drop the root
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger))
        {
            if (_state == RootState.Held)
            {
                this.transform.SetParent(_oldParent);
                foreach (Collider c in GetComponentsInChildren<Collider>())
                    c.enabled = true;
                _rigidbody = this.gameObject.AddComponent<Rigidbody>();

                if (_leftHandRigidbody && _rigidbody)
                {
                    Vector3 newpos = _leftHandAnchor.transform.position;
                    Vector3 diff = newpos - _lastPos;
                    _rigidbody.velocity = diff / Time.deltaTime;
                    _rigidbody.angularVelocity = Vector3.one * 2;
                }
                _rigidbody.useGravity = true;
                this.GetComponent<Collider>().isTrigger = false;

                this.GetComponentInParent<ToothBehaviour>().StartCoroutine(this.GetComponentInParent<ToothBehaviour>().RegenerateTooth());
            }

            _state = RootState.Released;


        }

        // Pull it out
        if (_state == RootState.Grabbed) 
        {
            Vector3 closestPoint = Vector3.Normalize(_leftHandAnchor.transform.position - this.transform.position);
            this.transform.rotation = _currentRot * Quaternion.FromToRotation(_startPos, closestPoint);

            float deltaAngle = Quaternion.Angle(_lastRot, _leftHandAnchor.transform.rotation);
            _accumulatedDeltaRotation += deltaAngle;
            _lastRot = _leftHandAnchor.transform.rotation;

            if (_yOffsetGameObject)
                _yOffsetGameObject.transform.localPosition = new Vector3(0f, 0.3f * _accumulatedDeltaRotation / _maxWiggleToPull, 0f);

            if (_accumulatedDeltaRotation >= _maxWiggleToPull)
            {
                _oldParent = this.transform.parent;
                this.transform.SetParent(_leftHandAnchor.transform);
                this.transform.position = _leftHandAnchor.transform.position; // oh snap!
                _state = RootState.Held;

                foreach (Collider c in GetComponentsInChildren<Collider>())
                    c.enabled = false;
                DragonVoice.Instance.PlayDragonHurtSound();
                Instantiate(destructionEffect, _yOffsetGameObject.transform.position, Quaternion.LookRotation(-this.transform.up));
            }
            
        }

        _lastPos = _leftHandAnchor.transform.position;
    }

    public void resetToothRoot()
    {
        if(_rigidbody) 
            Destroy(_rigidbody);

        if (_yOffsetGameObject)
            _yOffsetGameObject.transform.localPosition = Vector3.zero;

        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        _accumulatedDeltaRotation = 0f;
        _isPullable = false;
        _state = RootState.Released;
    }
}
