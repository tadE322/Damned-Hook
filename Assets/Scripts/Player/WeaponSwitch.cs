using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject pistol;
    public GameObject sword;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && pistol.activeInHierarchy)
        {
            pistol.SetActive(false);
            sword.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Q) && sword.activeInHierarchy)
        {
            pistol.SetActive(true);
            sword.SetActive(false);
        }
    }
}
