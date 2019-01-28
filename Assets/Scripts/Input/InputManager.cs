using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homebound.Hardware 
{
    public class InputManager : MonoBehaviour 
    {

        private enum InputLockStatus 
        {
            None,
            Locked, 
            Unlocked
        }

        enum GameplayInputType
        {
            PC,
            Mobile
        }

        public static InputManager instance;
        public HardwareInput gameInput { get; private set; }

        [SerializeField]
        private GameplayInputType _inputType;
        private InputLockStatus _inputLockStatus = InputLockStatus.None;

        private void Awake() 
        {

            // Default the input lock to unlocked 
            _inputLockStatus = InputLockStatus.Unlocked;

            if (instance == null) 
            {
                instance = this;
            } 
            else if (instance != null) 
            {
                Debug.Log("Found an existing instance of the InputManager, destroying this one");
                DestroyImmediate(this);
            }

            switch (_inputType) 
            {
                case GameplayInputType.PC: 
                    {
                        gameInput = new PCInput();
                        break;
                    }
                case GameplayInputType.Mobile: 
                    {
                        Debug.LogError("Selected input type is not supported!");
                        break;
                    }
            }
        }

        public void LockInput() 
        {
            _inputLockStatus = InputLockStatus.Locked;
        }

        public void UnlockInput() 
        {
            _inputLockStatus = InputLockStatus.Unlocked;
        }

        private void Update() 
        {
            if (_inputLockStatus == InputLockStatus.Unlocked) 
            {
                gameInput.GetPrimaryButtonStatus();
                gameInput.GetSecondaryButtonStatus();
            }
        }
    }

}