using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colider : MonoBehaviour, IInteractable
{
    public void Expand()
    {
       this.transform.localScale = transform.localScale * 2;
    }

    public void Interact()
    {
        print("Press E");
    }
}
