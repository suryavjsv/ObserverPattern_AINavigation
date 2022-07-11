using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeOrb : MonoBehaviour
{
    //Using ACTION component, we can simplify this code without getting any uneccesory references
    public static event Action OnOrbCollected;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "HK_AllAnim"){
            
            /* if(OnOrbCollected != null)
                OnOrbCollected.Invoke();  or*/
            
            OnOrbCollected?.Invoke();
            Destroy(gameObject);

        }
    }
}
