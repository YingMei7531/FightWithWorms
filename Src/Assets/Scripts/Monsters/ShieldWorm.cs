using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWorm : Monster
{
    public float def;

    override public void Hit(float atk, bool isBreak = false) {
        if (isBreak) hp -= atk;
        else {
            hp -= Mathf.Max(0f, atk - def);
        }
    }

    override protected void Skill() {
        return;
    }
}
