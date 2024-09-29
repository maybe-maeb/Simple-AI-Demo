using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool isBroken;
    public Material unbroken;
    public Material broken;

    public void Update(){
        if (isBroken) GetComponent<Renderer>().material = broken;
        else GetComponent<Renderer>().material = unbroken;
    }
}
