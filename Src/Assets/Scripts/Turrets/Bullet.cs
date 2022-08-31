using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float atk;
    public bool isBreak;
    public GameObject blastEffect;

    List<GameObject> monsters = new List<GameObject>();

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (transform.position.y >= 1.6) Blast();
    }

    void Blast() {
        foreach (GameObject monster in monsters) {
            if (monster == null) continue;
            monster.GetComponent<Monster>().Hit(atk, isBreak);
        }
        GameObject effect = Instantiate(blastEffect, transform.position, blastEffect.transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent != null) {
            GameObject gameObject = other.transform.parent.gameObject;
            if (gameObject.tag == "FlyMonster") {
                monsters.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent != null) {
            GameObject gameObject = other.transform.parent.gameObject;
            if (gameObject.tag == "FlyMonster") {
                monsters.Remove(gameObject);
            }
        }
    }
}
