using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public Transform turrets;
    public Turret[] turretPrefabs;

    public GameObject buildEffect;

    public GameObject selectedBlock;

    void Awake()
    {
        instance = this;
        selectedBlock = null;
    }

    void Update()
    {
        if (!GameManager.instance.GetGameState()) return;
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false) {
            DestroySelectedBlock();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("MapBlock"))) {
                SwitchSelectedBlock(hit.collider.gameObject);
            }
        }
    }

    public bool BuildTurret(int index) {
        if (selectedBlock) {
            return selectedBlock.GetComponent<BuildBlock>().CreateTurret(turretPrefabs[index], turrets);
        }
        return false;
    }

    public bool DestroyTurret() { 
        if (selectedBlock) {
            return selectedBlock.GetComponent<BuildBlock>().DestroyTurret();
        }
        return false;
    }

    void DestroySelectedBlock() {
        if (selectedBlock != null) selectedBlock.GetComponent<BuildBlock>().SetStatus(false);
        selectedBlock = null;
    }
    void SwitchSelectedBlock(GameObject newSelectedBlock) {
        DestroySelectedBlock();
        selectedBlock = newSelectedBlock;
        selectedBlock.GetComponent<BuildBlock>().SetStatus(true);
    }
}
