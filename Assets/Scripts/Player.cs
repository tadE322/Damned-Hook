using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Unit
{
    //Переопределения работают так
    public override void Kill()
    {
        base.Kill();
        Debug.Log("Player killed. GAME OVER!");
    }
}
