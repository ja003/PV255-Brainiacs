using UnityEngine;
using System.Collections;

public class ControlKeysP4 : ControlKeys {

    public ControlKeysP4()
    {
        base.keyUp = KeyCode.I;
        base.keyLeft = KeyCode.J;
        base.keyDown = KeyCode.K;
        base.keyRight = KeyCode.L;
        base.keyFire = KeyCode.Space;
        base.keySwitchWeapon = KeyCode.B;
    }
}
