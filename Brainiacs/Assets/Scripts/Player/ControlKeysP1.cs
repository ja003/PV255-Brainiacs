using UnityEngine;
using System.Collections;

public class ControlKeysP1 : ControlKeys {

    public ControlKeysP1() {
        base.keyUp = KeyCode.W;
        base.keyLeft = KeyCode.A;
        base.keyDown = KeyCode.S;
        base.keyRight = KeyCode.D;
        base.keyFire = KeyCode.LeftControl;
        base.keySwitchWeapon = KeyCode.LeftShift;
    }
}
