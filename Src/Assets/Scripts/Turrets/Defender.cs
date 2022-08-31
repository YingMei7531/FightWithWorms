using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Turret
{
    public Transform firePos;
    public LineRenderer lineRenderer;

    protected GameObject target;

    override protected void Init() {
        target = null;
    }
    override protected void TargetJudge(GameObject monster) {
        if (monster.tag != "Monster") return;
        if ((monster.transform.position.x - transform.position.x) * (monster.transform.position.x - transform.position.x) +
            (monster.transform.position.z - transform.position.z) * (monster.transform.position.z - transform.position.z) < (radius * radius)) {
            if (target) {
                if (target.GetComponent<ShieldWorm>() != null && monster.GetComponent<ShieldWorm>() == null) target = monster;
            }
            else target = monster;
        }
    }
    override protected void Turn() {
        if (target == null) return;
        Vector3 lookForward = target.transform.position - transform.position;
        lookForward.y = 0f;
        transform.rotation = Quaternion.LookRotation(lookForward.normalized, Vector3.up);
    }
    override protected void Attack() {
        if (target == null) return;
        if (time >= deltaTime) {
            time = 0f;
            StartCoroutine(Lasing());
            target.GetComponent<Monster>().Hit(atk);
        }
    }

    protected IEnumerator Lasing() {
        lineRenderer.SetPosition(0, firePos.position);
        Vector3 targetPos = target.transform.position;
        lineRenderer.SetPosition(1, targetPos);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}