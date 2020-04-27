using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUpgrades : MonoBehaviour
{

    public static PlayerUpgrades Instance { get; private  set; }

    public bool explodeOnImpact;
    public bool ultimate;
    
    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

        explodeOnImpact = false;
        ultimate = false;
    }

    [ContextMenu("ActivateExplodeOnImpact")]
    public void ActivateExplodeOnImpact(int cost)
     {
             explodeOnImpact = true;
             PlayerLogicScript.Instance.money -= cost;
     }
    
    public void ActivateUltimate(int cost)
    {
        ultimate = true;
        PlayerLogicScript.Instance.money -= cost;
    }
}
