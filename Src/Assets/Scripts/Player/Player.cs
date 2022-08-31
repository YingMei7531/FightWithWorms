using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public int hp;
    public int money;

    void Start()
    {
        instance = this;
    }

    public void Hit() {
        hp--;
    }
    public void AddMoney(int value) {
        money += value;
    }
    public void PayMoney(int value) {
        money -= value;
    }
}
