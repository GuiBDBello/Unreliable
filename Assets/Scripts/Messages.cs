﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    public string[] messages;

    private void Start()
    {
        messages = new string[10];

        messages[0]  = "Just shoot to kill The Cubes!";
        messages[1]  = "The Cubes just want a hug!";
        messages[3]  = "Just give up! You can't kill The Cubes!";
        messages[2]  = "The Cubes can't kill themselves!";
        messages[4]  = "It's impossible to kill The Cubes!";
        messages[5]  = "Don't even bother trying to make The Cubes hit themselves.";
        messages[6]  = "Shoot the cubes to weaken them!";
        messages[7]  = "Hug The Cubes to gain health!";
        messages[8]  = "The more you shoot The Cubes, more friendly they get!";
        messages[9]  = "If The Cubes throw something at you, take it!";
    }
}
