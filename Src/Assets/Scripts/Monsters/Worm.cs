using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Monster
{
    override public void Hit(float atk, bool isBreak = false) {
        hp -= atk;
    }

    override protected void Skill() {
        return;
    }
}
