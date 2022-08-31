using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiaircraftDefender : Defender
{
    override protected void TargetJudge(GameObject monster) {
        if (target) return;
        if (monster.tag != "FlyMonster") return;
        if ((monster.transform.position.x - transform.position.x) * (monster.transform.position.x - transform.position.x) +
            (monster.transform.position.z - transform.position.z) * (monster.transform.position.z - transform.position.z) < (radius * radius)) {
            target = monster;
        }
    }
    override protected void Turn() {
        return;
    }
}
