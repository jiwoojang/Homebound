using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebound.Hardware;

namespace Homebound.Player 
{

    [System.Serializable]
    public class PlayBounds 
    {
        [SerializeField]
        private Transform leftBound;
        [SerializeField]
        private Transform rightBound;

        public Vector3 GetLeftBound() 
        {
            return leftBound.transform.position;
        }

        public Vector3 GetRightBound() 
        {
            return rightBound.transform.position;
        }
    }

    public class PlayerController : MonoBehaviour 
    {

        public float driftSpeed;
        public Transform itemDropTransform;

        [SerializeField]
        private float _bumpFrequency = 30.0f;

        [SerializeField]
        private float _bumpMagnitude = 1.0f;

        [SerializeField]
        private PlayBounds _bounds;
        private Rigidbody _rigidbody;
        private float _avgYPosition;
        private bool _atLeftBoundary;
        private bool _atRightBoundary;

        public bool stopVibration;

        private void Awake() 
        {
            _rigidbody = GetComponent<Rigidbody>();

            if (_rigidbody == null)
            {
                Debug.LogError("Could not find rigidbody!");
            }

            _avgYPosition = _rigidbody.position.y;
        }

        private void OnEnable() 
        {
            if (InputManager.instance != null){
                InputManager.instance.gameInput.OnPrimaryButtonPress += OnLeftKeyStarted;
                InputManager.instance.gameInput.OnSecondaryButtonPress += OnRightKeyStarted;
            } 
            else 
            {
                Debug.Log("Input Manager could not be found");
            }
        }

        private void FixedUpdate() 
        {
            // Clamp position via bounds
            float xPosition = Mathf.Clamp(transform.position.x, _bounds.GetLeftBound().x, _bounds.GetRightBound().x);

            if ((xPosition / _bounds.GetLeftBound().x) > 0.95f)
            {
                if (!_atLeftBoundary)
                {
                    _atLeftBoundary = true;
                    _rigidbody.velocity = Vector3.zero;
                }
            } 
            else 
            {
                if (_atLeftBoundary)
                    _atLeftBoundary = false;

            }

            if ((xPosition / _bounds.GetRightBound().x) > 0.95f)
            {
                if (!_atRightBoundary)
                {
                    _atRightBoundary = true;
                    _rigidbody.velocity = Vector3.zero;
                }
            } 
            else
            {
                if (_atRightBoundary)
                    _atRightBoundary = false;
            }
            float yPosition;
            if (!stopVibration)
                yPosition = _bumpMagnitude * (Mathf.Sin(_bumpFrequency * Time.time) + 1);
            else
                yPosition = 0;
            _rigidbody.position = new Vector3(xPosition, _avgYPosition + yPosition, _rigidbody.position.z);

            // Apply the rocking rotation
            _rigidbody.rotation = Quaternion.Euler(0, 0, -1 * _rigidbody.velocity.x);
        }

        void OnLeftKeyStarted(HardwareInput sender)
        {
            if (!_atLeftBoundary)
            {
                _rigidbody.AddForce(Vector3.left * driftSpeed);
            }
        }

        void OnRightKeyStarted(HardwareInput sender) 
        {
            if (!_atRightBoundary) {
                _rigidbody.AddForce(Vector3.right * driftSpeed);
            }
        }
    }
}