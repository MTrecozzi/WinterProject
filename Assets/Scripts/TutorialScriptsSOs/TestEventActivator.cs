using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventActivator : MonoBehaviour
{

    GameEvent pickUpCube;


    
    public void EventGoBRRR()
    {
        pickUpCube.Raise();


    }

}
