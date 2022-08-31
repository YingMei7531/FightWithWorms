using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public TextAsset levelFile;
    List<string> levelData = new List<string>();
    int levelNum = 0;
    int levelIndex = 0;

    public Monster[] monsterPrefabs;
    public Transform brithPos;
    public Transform targetPos;
    public Transform enemys;

    public float levelDeltaTime;
    List<Monster> curMonsterPrefabs = new List<Monster>();
    List<int> curMonsterNums = new List<int>();
    int monsterGroupNums;
    float deltaTime;
    bool isMonstersClear;

    void Start()
    {
        instance = this;

        var lineData = levelFile.text.Split('\n');
        foreach (var line in lineData) {
            levelData.Add(line.Trim());
        }
        levelNum = levelData.Count / 3;
        levelIndex = 0;
        isMonstersClear = true;
    }

    void Update()
    {
        if (isMonstersClear && levelIndex < levelNum) {
            NextLevel();
        }
    }

    void NextLevel() {
        int index = levelIndex * 3;
        var lineData = levelData[index++].Split(' ');
        int.TryParse(lineData[0], out monsterGroupNums);
        float.TryParse(lineData[1], out deltaTime);
        curMonsterPrefabs.Clear();
        foreach (var num in levelData[index++].Split(' ')) {
            int p = 0;
            int.TryParse(num, out p);
            curMonsterPrefabs.Add(monsterPrefabs[p]);
        }
        curMonsterNums.Clear();
        foreach (var num in levelData[index++].Split(' ')) {
            int p = 0;
            int.TryParse(num, out p);
            curMonsterNums.Add(p);
        }
        levelIndex++;
        isMonstersClear = false;
        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster() {
        yield return new WaitForSeconds(levelDeltaTime);
        for (int index = 0; index < monsterGroupNums; index++) {
            for (int i = 0; i < curMonsterPrefabs.Count; i++) {
                for (int j = 0; j < curMonsterNums[i]; j++) {
                    Monster monster = Instantiate(curMonsterPrefabs[i], brithPos.position, brithPos.rotation, enemys);
                    monster.SetTargetPos(targetPos.position);
                    yield return new WaitForSeconds(deltaTime);
                }
            }
        }
        isMonstersClear = true;
    }

    public int GetLevel() {
        return levelIndex;
    }

    public bool IsEnemysEnd() {
        return (levelIndex == levelNum && enemys.childCount == 0 && isMonstersClear);
    }
}
