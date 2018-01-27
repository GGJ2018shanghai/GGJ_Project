using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerData : ScriptableObject
{
    public bool logDebugInfo = false;
    public bool invertYAxis = false;
    public bool useFixedUpdate = false;
    public bool suspendInBackground = false;
}
