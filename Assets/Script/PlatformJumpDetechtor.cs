using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformJumpDetechtor : MonoBehaviour
{
    private FixedJoint2D connectedPlayerJoint;

    public void SetConnectedPlayerJoint(FixedJoint2D joint)
    {
        connectedPlayerJoint = joint;
    }

    private void Update()
    {
        if (connectedPlayerJoint != null && Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump button pressed on Joypad!");
            Destroy(connectedPlayerJoint);
            connectedPlayerJoint = null;
        }
    }

}
