using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVanHealth : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField]
    private bool _shouldUseAmbientHealthDrain;
    [SerializeField]
    private float _ambientHealthDrain;

    [SerializeField]
    public float _maxHealth = 100.0f;

    [SerializeField]
    private int _numberOfHealthLevels = 5;

    [SerializeField]
    EndCamera endCamera;

    private int _currentHealthLevel;
    public float _currentHealth;

    public delegate void VanHealthLevelChanged(int newHealthLevel, int maxHealthLevel);
    public event VanHealthLevelChanged OnVanHealthLevelChanged;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _currentHealthLevel = _numberOfHealthLevels;
    }

    public void AddPlayerVanHealth(float reductionValue)
    {
        _currentHealth += reductionValue;
        CheckNewVanHealthState();
    }

    private void CheckNewVanHealthState() 
    {

        if (_currentHealth < 0)
        {
            Debug.Log("Player has reached 0 health");
            endCamera.GameOverSequenceMethod();
            _shouldUseAmbientHealthDrain = false;
            return;
        }

        // Lowest bracket is 1, highest bracket is the specified number
        int vanHealthBracket = Mathf.CeilToInt(_currentHealth / (_maxHealth/_numberOfHealthLevels));

        if (vanHealthBracket < _currentHealthLevel) 
        {
            _currentHealthLevel = vanHealthBracket;

            if (OnVanHealthLevelChanged != null)
            {
                OnVanHealthLevelChanged(_currentHealthLevel, _numberOfHealthLevels);
                //Debug.Log("Health level changed to " + _currentHealth);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldUseAmbientHealthDrain)
        {
            _currentHealth -= _ambientHealthDrain * Time.deltaTime;
            CheckNewVanHealthState();
            //Debug.Log("Current Health Level: " + _currentHealth);
        }
    }
}
