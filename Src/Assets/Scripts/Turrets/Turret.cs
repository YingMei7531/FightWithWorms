using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public string turretName;
    public TextAsset turretNote;
    public bool isBase;
    public Turret[] upgradeOptions;
    public float radius;
    public float atk;
    public int value;
    public float deltaTime;
    protected float time = 0f;

    GameObject monsters;

    void Awake()
    {
        monsters = EnemyManager.instance.enemys.gameObject;
    }

    void Update()
    {
        time += Time.deltaTime;
        Init();
        foreach (Transform monster in monsters.transform) {
            TargetJudge(monster.gameObject);
        }
        Turn();
        Attack();
    }

    abstract protected void Init();
    abstract protected void TargetJudge(GameObject monster);
    abstract protected void Turn();
    abstract protected void Attack();
}
