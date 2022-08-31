using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CreateMap : MonoBehaviour
{
    public TextAsset mapFile;
    public GameObject groundPrefab;
    public GameObject roadBlock, buildBlock, birthPos, homePos;

    void OnEnable()
    {
        GameObject map = new GameObject("Map");
        GameObject ground = Instantiate(groundPrefab, map.transform);
        map.name = "Map";
        var lineData = mapFile.text.Split('\n');
        for (int i = 0; i < lineData.Length; i++) {
            for (int j = 0; j < lineData[i].Length; j++) {
                int num;
                if (int.TryParse(lineData[i][j].ToString(), out num)) {
                    if (num == 1) {
                        GameObject block = Instantiate(buildBlock);
                        block.transform.position = new Vector3(j, 0, -i);
                        block.name = $"BuildBlock[{i}][{j}]";
                        block.transform.parent = map.transform;
                    }
                    else {
                        GameObject block = Instantiate(roadBlock);
                        block.transform.position = new Vector3(j, 0, -i);
                        block.name = $"RoadBlock[{i}][{j}]";
                        block.transform.parent = map.transform;
                    }
                    if (num == 2) {
                        GameObject birth = Instantiate(birthPos);
                        birth.transform.position = new Vector3(j, 1, -i);
                        birth.name = "BirthPos";
                    }
                    else if (num == 3) {
                        GameObject home = Instantiate(homePos);
                        home.transform.position = new Vector3(j, 0, -i);
                        home.name = "HomePos";
                    }
                }
            }
        }
    }
}
