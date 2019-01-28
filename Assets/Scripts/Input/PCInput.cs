using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Homebound.Hardware {
    public class PCInput : HardwareInput {
        private KeyCode _primaryButton = KeyCode.A;
        private KeyCode _secondaryButton = KeyCode.D;

        public override void GetPrimaryButtonStatus() {
            if (Input.GetKeyDown(_primaryButton)) {
                FirePrimaryButtonStarted();
                Debug.Log("A pressed");
            } else if (Input.GetKey(_primaryButton)) {
                FirePrimaryButtonPress();
            } else if (Input.GetKeyUp(_primaryButton)) {
                FirePrimaryButtonEnded();
            }
        }

        public override void GetSecondaryButtonStatus() {
            if (Input.GetKeyDown(_secondaryButton)) {
                FireSecondaryButtonStarted();
                Debug.Log("D pressed");
            } else if (Input.GetKey(_secondaryButton)) {
                FireSecondaryButtonPress();
            } else if (Input.GetKeyUp(_secondaryButton)) {
                FireSecondaryButtonEnded();
            }
        }
    }
}