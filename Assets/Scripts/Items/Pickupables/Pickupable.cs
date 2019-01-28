using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebound.Player;

public class Pickupable : MonoBehaviour, IPickupable
{
    [SerializeField]
    private float _forceOnCollected = 10.0f;

    [SerializeField]
    private float _scaleDownOnPickup = 0.25f;

    [SerializeField]
    private ParticleSystem _pickupEffect;

    ScorePerSecond scoreScript;
    GlobalSpeed globalSpeed;

    private TerrainMovement _movement;
    private Rigidbody _rigidbody;
    public AudioSource pickUp;

    private void Awake() 
    {
        
        _rigidbody = GetComponent<Rigidbody>();
        pickUp = GetComponent<AudioSource>();
        if (_rigidbody == null)
        {
            Debug.LogError("Please attatch a rigidbody to this item!");
        }

        _movement = GetComponent<TerrainMovement>();
    }

    private void Start()
    {
        scoreScript = GameObject.FindGameObjectWithTag("ScoreScriptObj").GetComponent<ScorePerSecond>();
        globalSpeed = GameObject.FindGameObjectWithTag("GlobalSpeed").GetComponent<GlobalSpeed>();
    }

    public virtual void PickupEffect()
    {
        if (_pickupEffect) 
        {
            _pickupEffect.Play();
            Destroy(_pickupEffect.gameObject, 1.0f);
        }
    }

    public void DropObjectOnPlayer(GameObject playerObject)
    {
        PlayerController controller = playerObject.GetComponent<PlayerController>();

        if (controller != null) 
        {

            if(_movement != null)
            {
                _movement.enabled = false;
            }

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            transform.position = controller.itemDropTransform.position;
            transform.localScale = transform.localScale * _scaleDownOnPickup;
            //_rigidbody.AddForce(Vector3.back * _forceOnCollected);
        } 
        else
        {
            Debug.LogError("Cannot place item at player item transform");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreScript.scoreAmount += 5;
            globalSpeed.speed += 0.2f;
            DropObjectOnPlayer(other.gameObject);
            PickupEffect();
            pickUp.Play();
            // Destroy(gameObject);
        }
    }
}
