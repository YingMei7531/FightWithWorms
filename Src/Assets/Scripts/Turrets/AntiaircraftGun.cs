using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiaircraftGun : Turret
{
    public Transform gun;
    public Transform firePos;
    public Bullet bulletPrefab;

    GameObject target;

    override protected void Init() {
        target = null;
    }
    override protected void TargetJudge(GameObject monster) {
        if (target) return;
        if (monster.tag != "FlyMonster") return;
        if ((monster.transform.position.x - transform.position.x) * (monster.transform.position.x - transform.position.x) +
            (monster.transform.position.z - transform.position.z) * (monster.transform.position.z - transform.position.z) < (radius * radius)) {
            target = monster;
        }
    }
    override protected void Turn() {
        if (target == null) return;
        Vector3 lookForward = target.transform.position - transform.position;
        lookForward.y = 0f;
        transform.rotation = Quaternion.LookRotation(lookForward.normalized, Vector3.up);
        float theta = IncludedAngle(transform.forward, (target.transform.position - transform.position).normalized);
        gun.localRotation = Quaternion.Euler(90f - theta, 0f, 0f);
    }
    override protected void Attack() {
        if (target == null) return;
        if (time >= deltaTime) {
            time = 0f;
            Bullet bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            bullet.atk = atk;
        }
    }

    float IncludedAngle(Vector3 a, Vector3 b) {
        float cosTheta = Vector3.Dot(a, b) / (a.magnitude * b.magnitude);
        float theta = Mathf.Acos(cosTheta) * Mathf.Rad2Deg;
        return theta;
    }
}
