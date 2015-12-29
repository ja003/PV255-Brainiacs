using UnityEngine;
using System.Collections;

public class ControlKeysP3 : ControlKeys
{

    public ControlKeysP3()
    {
        base.keyUp = KeyCode.UpArrow;
        base.keyLeft = KeyCode.LeftArrow;
        base.keyDown = KeyCode.DownArrow;
        base.keyRight = KeyCode.RightArrow;
        base.keyFire = KeyCode.RightControl;
        base.keySwitchWeapon = KeyCode.RightShift;
    }
}
