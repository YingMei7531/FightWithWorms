using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Monster : MonoBehaviour
{
    public HitPoint hitPointPrefab;
    HitPoint hitPoint;
    public Transform hitPointPos;
    public GameObject deadEffect;
    public int value;
    public float totalHp;
    protected float hp;
    public float deltaTime;
    protected float time = 0f;

    void Awake()
    {
        hitPoint = Instantiate(hitPointPrefab, hitPointPos.position, Quaternion.identity, hitPointPos);
        hp = totalHp;
    }

    void Update()
    {
        if (hp < 0f) Die();
        hitPoint.SetValue(hp / totalHp);
        time += Time.deltaTime;
        if (time >= deltaTime) {
            time = 0f;
            Skill();
        }
    }

    public void SetTargetPos(Vector3 target) {
        GetComponent<NavMeshAgent>().destination = target;
    }

    void Die() {
        GameObject effect = Instantiate(deadEffect, transform.position, deadEffect.transform.rotation);
        Destroy(effect, 1f);
        Player.instance.AddMoney(value);
        Destroy(this.gameObject);
    }

    abstract public void Hit(float atk, bool isBreak = false);

    abstract protected void Skill();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Player.instance.Hit();
            Destroy(this.gameObject);
        }
    }
}
