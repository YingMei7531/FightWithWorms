using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool isRun;
    bool isWin;

    void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
        isRun = true;
        isWin = false;
    }

    void Update()
    {
        if (Player.instance.hp <= 0) {
            Time.timeScale = 0f;
            isRun = false;
        }
        else if (EnemyManager.instance.IsEnemysEnd()) {
            Time.timeScale = 0f;
            isRun = false;
            isWin = true;
        }
    }

    public bool GetGameState() {
        return isRun;
    }
    public bool GetGameEnd() {
        return isWin;
    }
}
