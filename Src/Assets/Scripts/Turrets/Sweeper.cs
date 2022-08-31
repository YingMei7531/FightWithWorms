using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : Turret
{
    public Transform[] firePos;
    public Missile missilePrefab;

    int targetNum;
    List<GameObject> targets = new List<GameObject>();
    List<float> targetsDis = new List<float>();

    override protected void Init() {
        targets.Clear();
        targetsDis.Clear();
        targetNum = firePos.Length;
    }
    override protected void TargetJudge(GameObject monster) {
        if (monster.tag != "Monster") return;
        float sqrDistance = GetSqrDistance(monster.transform);
        if (sqrDistance >= (radius * radius)) return;
        if (monster.GetComponent<WormMother>() != null) {
            for (int i = 0; i < targets.Count; i++) {
                targets[i] = monster;
                targetsDis[i] = radius * radius + 1;
            }
            while(targets.Count< targetNum) {
                targets.Add(monster);
                targetsDis.Add(radius * radius + 1);
            }
            return;
        }
        if (targets.Count >= targetNum) {
            for (int i = 0; i < targets.Count; i++) {
                if (sqrDistance > targetsDis[i]) {
                    targets[i] = monster;
                    targetsDis[i] = sqrDistance;
                    break;
                }
            }
        }
        else {
            targets.Add(monster);
            targetsDis.Add(sqrDistance);
        }
    }
    override protected void Turn() {
        if (targets.Count == 0) return;
        Vector3 LookForward = targets[0].transform.position - transform.position;
        LookForward.y = 0f;
        transform.rotation = Quaternion.LookRotation(LookForward.normalized, Vector3.up);
    }
    override protected void Attack() {
        if (targets.Count == 0) return;
        if (time >= deltaTime) {
            time = 0f;
            for (int i = 0; targets.Count < targetNum; i++) targets.Add(targets[i]);
            for (int i = 0; i < targets.Count; i++) {
                if(targets[i]!=null) {
                    Missile missile = Instantiate(missilePrefab, firePos[i].position, firePos[i].rotation);
                    missile.target = targets[i].transform;
                    missile.atk = this.atk;
                }
            }
        }
    }

    float GetSqrDistance(Transform target) {
        return (target.position.x - transform.position.x) * (target.position.x - transform.position.x) +
            (target.position.z - transform.position.z) * (target.position.z - transform.position.z);
    }
}
