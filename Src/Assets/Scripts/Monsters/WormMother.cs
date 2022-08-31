using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WormMother : Monster
{
    public Transform birthPos;
    public Monster childPrefab;

    GameObject monsters;

    override public void Hit(float atk, bool isBreak = false) {
        hp -= atk;
    }

    override protected void Skill() {
        monsters = GameObject.Find("Enemys");
        Monster child = Instantiate(childPrefab, birthPos.position, transform.rotation, monsters.transform);
        child.SetTargetPos(GetComponent<NavMeshAgent>().destination);
    }
}
