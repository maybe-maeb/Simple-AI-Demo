using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public Breakable computer;
    public Breakable toilet;
    public Breakable sink;
    public Breakable lamp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) computer.isBroken = true;
        if (Input.GetKeyDown(KeyCode.S)) toilet.isBroken = true;
        if (Input.GetKeyDown(KeyCode.D)) sink.isBroken = true;
        if (Input.GetKeyDown(KeyCode.F)) lamp.isBroken = true;
    }
}
