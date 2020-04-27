using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBoxActivator : Activator
{

    private void OnDestroy()
    {
        activate();
    }

}
