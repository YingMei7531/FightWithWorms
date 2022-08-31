using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyWorm : Monster
{
    public float fallTime;
    public float baseOffset, flyOffset;
    bool isFly = true;

    override public void Hit(float atk, bool isBreak = false) {
        hp -= atk;
        if (isBreak) {
            FallDown();
        }
    }

    override protected void Skill() {
        if (!isFly) {
            gameObject.tag = "FlyMonster";
            isFly = true;
            GetComponent<NavMeshAgent>().baseOffset = flyOffset;
        }
    }

    void FallDown() {
        gameObject.tag = "Monster";
        time = -fallTime;
        isFly = false;
        GetComponent<NavMeshAgent>().baseOffset = baseOffset;
    }

    public bool GetFlyStatus() {
        return isFly;
    }
}
