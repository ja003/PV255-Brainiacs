using UnityEngine;
using System.Collections;

public class ControlKeysP2 : ControlKeys {

    public ControlKeysP2() {
        base.keyUp = KeyCode.Keypad8;
        base.keyLeft = KeyCode.Keypad4;
        base.keyDown = KeyCode.Keypad2;
        base.keyRight = KeyCode.Keypad6;
        base.keyFire = KeyCode.Keypad1;
        base.keySwitchWeapon = KeyCode.Keypad0;
    }
}
