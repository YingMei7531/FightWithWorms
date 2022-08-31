using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform target;
    public float deltaAngle;
    public float speed;
    public float atk;
    public GameObject blastEffect;

    void Update()
    {
        if (target == null) {
            Blast();
            return;
        }
        Vector3 toTarget = target.position - transform.position;
        Vector3 lookRotation = Quaternion.LookRotation(toTarget).eulerAngles;
        Vector3 curRotation = transform.up;
        curRotation = Vector3.MoveTowards(curRotation.normalized, toTarget.normalized, deltaAngle * Time.deltaTime);
        transform.up = curRotation;

        transform.position += transform.up * speed * Time.deltaTime;
    }

    void Blast() {
        GameObject effect = Instantiate(blastEffect, transform.position, blastEffect.transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.parent != null) { 
            GameObject gameObject = collision.transform.parent.gameObject;
            if (gameObject.tag == "Monster" || gameObject.tag == "FlyMonster") {
                gameObject.GetComponent<Monster>().Hit(atk);
                Blast();
            }
        }
    }
}
