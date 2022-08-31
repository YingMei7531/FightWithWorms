using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Turret
{
    public float angle;

    public Transform firePos;
    public ParticleSystem particleSystem;

    List<GameObject> targets = new List<GameObject>();

    override protected void Init() {
        targets.Clear();
        particleSystem.enableEmission = false;
    }
    override protected void TargetJudge(GameObject monster) {
        if (monster.tag != "Monster") return;
        if ((monster.transform.position.x - transform.position.x) * (monster.transform.position.x - transform.position.x) +
            (monster.transform.position.z - transform.position.z) * (monster.transform.position.z - transform.position.z) > (radius * radius)) {
            return;
        }
        if (targets.Count == 0) targets.Add(monster);
        else {
            Vector3 beginDir = targets[0].transform.position - firePos.transform.position;
            Vector3 endDir = targets[targets.Count - 1].transform.position - firePos.transform.position;
            Vector3 targetDir = monster.transform.position - firePos.transform.position;
            targetDir.y = beginDir.y = endDir.y = 0;
            float toBeginAngle = IncludedAngle(beginDir, targetDir);
            float toEndAngle = IncludedAngle(endDir, targetDir);
            if (Mathf.Abs(angle - (toBeginAngle + toEndAngle)) < 0.00001) {
                targets.Insert(targets.Count - 1, monster);
            }
            else if (toBeginAngle < toEndAngle && toEndAngle < angle) {
                targets.Insert(0, monster);
            }
            else if(toBeginAngle >= toEndAngle && toBeginAngle < angle) {
                targets.Add(monster);
            }
        }
    }
    override protected void Turn() {
        if (targets.Count == 0) return;
        Vector3 beginDir = targets[0].transform.position - firePos.transform.position;
        Vector3 endDir = targets[targets.Count - 1].transform.position - firePos.transform.position;
        beginDir.y = endDir.y = 0;
        Vector3 LookForward = beginDir.normalized + endDir.normalized;
        transform.rotation = Quaternion.LookRotation(LookForward.normalized, Vector3.up);
    }
    override protected void Attack() {
        if (targets.Count == 0) return;
        particleSystem.enableEmission = true;
        if (time >= deltaTime) {
            time = 0f;
            for (int i = 0; i < targets.Count; i++) {
                if(targets[i]!=null) targets[i].GetComponent<Monster>().Hit(atk);
            }
        }
    }

    float IncludedAngle(Vector3 a, Vector3 b) {
        float cosTheta = Vector3.Dot(a, b) / (a.magnitude * b.magnitude);
        float theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
        return theta;
    }
}