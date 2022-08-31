using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperDefender : Defender
{
    override protected void TargetJudge(GameObject monster) {
        if (monster.tag != "Monster") return;
        if ((monster.transform.position.x - transform.position.x) * (monster.transform.position.x - transform.position.x) +
            (monster.transform.position.z - transform.position.z) * (monster.transform.position.z - transform.position.z) < (radius * radius)) {
            if (target) {
                if (target.GetComponent<ShieldWorm>() == null && monster.GetComponent<ShieldWorm>() != null) target = monster;
            }
            else target = monster;
        }
    }
    override protected void Attack() { 
        if (target == null) return;
        if (time >= deltaTime) {
            time = 0f;
            StartCoroutine(Lasing());
            if(target.GetComponent<ShieldWorm>()!=null) target.GetComponent<Monster>().Hit(atk, true);
            else target.GetComponent<Monster>().Hit(atk);
        }
    }
}
