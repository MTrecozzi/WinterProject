using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;


public interface IHandleFistStuff
{
    void Push(Vector3 Direction);

    void TakeDamage(float damage);

    void Grab();
}



public class Stuff : MonoBehaviour
{

}

